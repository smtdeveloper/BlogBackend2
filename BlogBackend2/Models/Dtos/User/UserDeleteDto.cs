using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogBackend2.Models.Dtos.User
{
    public class UserDeleteDto
    {
        public int Id { get; set; }
        public int UserRoleId { get; set; }
    }
}
