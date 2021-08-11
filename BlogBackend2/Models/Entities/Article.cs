using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogBackend2.Models.Entities
{
    public class Article
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual List<Category> Categories { get; set; }
    }
}
