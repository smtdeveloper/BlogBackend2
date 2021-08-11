using System.Collections.Generic;

namespace BlogBackend2.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual List<Article> Articles { get; set; }
    }
}
