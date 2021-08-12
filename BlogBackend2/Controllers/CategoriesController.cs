using BlogBackend2.EntityFramework.DbContexts;
using BlogBackend2.Models.Dtos;
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
        public PostgreDbContext dbContext;

        public CategoriesController(PostgreDbContext dbContext)
        {
            this.dbContext = dbContext;
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

            if (dbContext.Categories.Any(p => p.Name.Equals(addDto.Name)))
                return BadRequest(new Result(success: false,Messages.CategoryNotFound));

            dbContext.Add(category);
            bool addResult = dbContext.SaveChanges() > 0;
            if (addResult)
                return Ok(new Result(success: true, message: Messages.CategoryNotAdded));

            return BadRequest(new Result(success: false, message: Messages.CategoryAdded));
        }

        [HttpGet("listwithoutjoin")]
        public IActionResult GetListWithoutJoin()
        {
            var query = from category in dbContext.Categories
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
            var categories = dbContext.Categories
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



    }
}
