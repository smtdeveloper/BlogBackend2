using System.Collections.Generic;

namespace BlogBackend2.Models.Entities
{
    public class UserRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public virtual List<User> Users { get; set; }
    }
}
