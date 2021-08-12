using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogBackend2.Models.Dtos.Article
{
    public class ArticleAddDto
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

    }
}
