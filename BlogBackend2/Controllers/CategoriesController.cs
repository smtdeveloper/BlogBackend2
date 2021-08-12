using BlogBackend2.EntityFramework.DbContexts;
using BlogBackend2.Models.Dtos;
using BlogBackend2.Models.Dtos.Category;
using BlogBackend2.Models.Entities;
using BlogBackend2.Utilities.Constants;
using BlogBackend2.Utilities.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BlogBackend2.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public PostgreDbContext _postgreDbContext;

        public CategoriesController(PostgreDbContext dbContext)
        {
            this._postgreDbContext = dbContext;
        }

        [HttpPost("add")]
        public IActionResult Add(CategoryAddDto addDto)
        {
            Category category = new Category()
            {
                Name = addDto.Name,
                IsActive = true,
                IsDeleted = false
            };

            if (_postgreDbContext.Categories.Any(p => p.Name.Equals(addDto.Name)))
                return BadRequest(new Result(success: false,Messages.CategoryNotFound));

            _postgreDbContext.Add(category);
            bool addResult = _postgreDbContext.SaveChanges() > 0;
            if (addResult)
                return Ok(new Result(success: true, message: Messages.CategoryAdded));

            return BadRequest(new Result(success: false, message: Messages.CategoryNotAdded));
        }

        [HttpPut("update")]
        public IActionResult Update(CategoryUpdateDto updateDto)
        {
            Category category = new Category()
            {
              Id = updateDto.Id,
              Name = updateDto.Name,
              IsActive = updateDto.IsActive,
              IsDeleted = updateDto.IsDeleted
                
               
            };

            _postgreDbContext.Update(category);
            bool addResult = _postgreDbContext.SaveChanges() > 0;
            if (addResult)
                return Ok(new Result(success: true, message: Messages.CategoryUpdated));

            return BadRequest(new Result(success: false, message: Messages.CategoryNotUpdated));
        }


        [HttpGet("listwithoutjoin")]
        public IActionResult GetListWithoutJoin()
        {
            var query = from category in _postgreDbContext.Categories
                        where category.IsDeleted == false
                        select new CategoryDto
                        {
                            Id = category.Id,
                            Name = category.Name,
                            IsActive = category.IsActive,
                            ArticleCount = category.Articles.Count
                        };

            return Ok(query.ToList());
        }


        [HttpGet("listwithinclude")]
        public IActionResult GetList()
        {
            var categories = _postgreDbContext.Categories
                .Include(p => p.Articles)
                .Where(p => p.IsDeleted == false).ToList();

            List<CategoryDto> categoryDtos = new();

            categories.ForEach(category =>
            {
                categoryDtos.Add(new CategoryDto()
                {
                    Id = category.Id,
                    Name = category.Name,
                    IsActive = category.IsActive,
                    ArticleCount = category.Articles.Count
                });
            });

            return Ok(new DataResult<List<CategoryDto>>(success: true, Messages.CategoryList, categoryDtos));
        }

        [HttpGet("getCategoryId")]
        public IActionResult GetCategoryId(int id)
        {
            var categories = _postgreDbContext.Categories
                .Include(p => p.Articles)
                .Where(p => p.Id == id).ToList();

            List<CategoryDto> categoryDtos = new();

            categories.ForEach(category =>
            {
                categoryDtos.Add(new CategoryDto()
                {
                    Id = category.Id,
                    Name = category.Name,
                    IsActive = category.IsActive,
                    ArticleCount = category.Articles.Count
                });
            });

            return Ok(new DataResult<List<CategoryDto>>(success: true, Messages.CategoryIdList, categoryDtos));
        }


    }
}
