using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogBackend2.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public int UserRoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public virtual List<Article> Articles { get; set; }

        [ForeignKey("UserRoleId")]
        public virtual UserRole UserRole { get; set; }
    }
}
