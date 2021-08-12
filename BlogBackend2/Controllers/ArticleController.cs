using BlogBackend2.EntityFramework.DbContexts;
using BlogBackend2.Models.Dtos.Article;
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
    public class ArticleController : ControllerBase
    {
        public PostgreDbContext _postgreDbContext;

        public ArticleController(PostgreDbContext postgreDbContext)
        {
            this._postgreDbContext = postgreDbContext;
        }

        [HttpPost("add")]
        public IActionResult Add(ArticleAddDto addDto)
        {
            Article article = new Article()
            {
                UserId = addDto.UserId,
                Title = addDto.Title,
                Content = addDto.Content,
                IsActive = true,
                IsDeleted = false
            };

            _postgreDbContext.Add(article);
            bool addResult = _postgreDbContext.SaveChanges() > 0;
            if (addResult)
                return Ok(new Result( true, Messages.ArticleAdded));

            return BadRequest(new Result( false,  Messages.ArticleNotAdded ));
        }

        [HttpPut("update")]
        public IActionResult Update(ArticleUpdateDto updateDto)
        {
            Article article = new Article()
            {
                Id =  updateDto.Id,
                UserId = updateDto.UserId,
                Title = updateDto.Title,
                Content = updateDto.Content,
                IsActive = updateDto.IsActive,
                IsDeleted = updateDto.IsDeleted
            };

            _postgreDbContext.Update(article);
            bool addResult = _postgreDbContext.SaveChanges() > 0;
            if (addResult)
                return Ok(new Result(true, Messages.ArticleUpdate));

            return BadRequest(new Result(false, Messages.ArticleNotUpdate));
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var article = _postgreDbContext.Articles
                .Where(p => p.IsDeleted == false).ToList();

            List<ArticleDto> articleDtos = new();

            article.ForEach(article =>
            {
                articleDtos.Add(new ArticleDto()
                {
                 Id = article.Id,
                 UserId = article.UserId,
                 Title = article.Title,
                 Content = article.Content,
                 IsActive = article.IsActive,
                 IsDeleted = article.IsDeleted
                });
            });

            return Ok(new DataResult<List<ArticleDto>>(true, Messages.ArticleList, articleDtos));
        }

        [HttpGet("getArticleId")]
        public IActionResult GetArticleId(int id)
        {
            var article = _postgreDbContext.Articles.Where(p => p.Id == id).ToList();

            List<ArticleDto> articleDto = new();

            article.ForEach(article =>
            {
                articleDto.Add(new ArticleDto()
                {
                    Id = article.Id,
                    UserId = article.UserId,
                    Title = article.Title,
                    Content = article.Content,
                    IsActive = article.IsActive,
                    IsDeleted = article.IsDeleted
                });
            });

            return Ok(new DataResult<List<ArticleDto>>(true, Messages.ArticleIdList, articleDto));

        }

    }
}
