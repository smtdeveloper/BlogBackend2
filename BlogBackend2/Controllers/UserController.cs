using BlogBackend2.EntityFramework.DbContexts;
using BlogBackend2.Models.Dtos.User;
using BlogBackend2.Models.Entities;
using BlogBackend2.Utilities.Constants;
using BlogBackend2.Utilities.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogBackend2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public PostgreDbContext _postgreDbContext;

        public UserController(PostgreDbContext postgreDbContext)
        {
            _postgreDbContext = postgreDbContext;
        }

        [HttpPost("add")]
        public IActionResult Add(UserAddDto userAddDto)
        {
            User user = new User()
            {
                UserRoleId = userAddDto.UserRoleId,
                FirstName = userAddDto.FirstName,
                LastName = userAddDto.LastName,
                UserName = userAddDto.UserName,
                Password = userAddDto.Password,
                IsActive = true
            };
            
            if (_postgreDbContext.Users.Any(u => u.UserName.Equals(userAddDto.UserName)))
                return BadRequest(new Result(false, Messages.UsernameIsUsed));

            _postgreDbContext.Add(user);
            bool Addresult = _postgreDbContext.SaveChanges() > 0;
            if (!Addresult)
                return BadRequest(new Result(false, Messages.UserNotAdded));

            return Ok(new Result(true, Messages.UserAdded));
        }

        [HttpPut("update")]
        public IActionResult Update(UserUpdateDto updateDto)
        {
            User user = new User()
            {
                Id = updateDto.Id,
                UserRoleId = updateDto.UserRoleId,
                FirstName = updateDto.FirstName,
                LastName = updateDto.LastName,
                UserName = updateDto.UserName,
                Password = updateDto.Password,
                IsActive = updateDto.IsActive
                
            };

            if (_postgreDbContext.Users.Any(u => u.UserName.Equals(updateDto.UserName)))
                return BadRequest(new Result(false, Messages.UsernameIsUsed));

            _postgreDbContext.Update(user);
            bool Addresult = _postgreDbContext.SaveChanges() > 0;
            if (!Addresult)
                return BadRequest(new Result(false, Messages.UserNotUpdate));

            return Ok(new Result(true, Messages.UserUpdate));
        }

        [HttpPut("delete")]
        public IActionResult Delete(UserDeleteDto deleteDto)
        {
            User user = new User()
            {
              Id = deleteDto.Id,
              UserRoleId = deleteDto.UserRoleId,
              IsActive = false
            };

            _postgreDbContext.Update(user);
            bool Addresult = _postgreDbContext.SaveChanges() > 0;
            if (!Addresult)
                return BadRequest(new Result(false, Messages.UserNotDelete));

            return Ok(new Result(true, Messages.UserDelete));
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
          var user =  _postgreDbContext.Users.Where(u => u.IsActive == true).ToList();

            List<UserDto> userDtos = new();

            user.ForEach(user =>
            {
                userDtos.Add(new UserDto()
                {
                    Id = user.Id,
                    UserRoleId = user.UserRoleId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Password = user.Password,
                    IsActive = user.IsActive
                   
                });
            });

            return Ok(new DataResult<List<UserDto>>( true, Messages.UserList, userDtos));
        }


        [HttpGet("getId")]
        public IActionResult GetId(int id)
        {
            var user = _postgreDbContext.Users.Where(u => u.Id == id).ToList();

            List<UserDto> userDtos = new();

            user.ForEach(user =>
            {
                userDtos.Add(new UserDto()
                {
                    Id = user.Id,
                    UserRoleId = user.UserRoleId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Password = user.Password,
                    IsActive = user.IsActive

                });
            });

            return Ok(new DataResult<List<UserDto>>(true, Messages.UserIdList, userDtos));
        }


    }
}
