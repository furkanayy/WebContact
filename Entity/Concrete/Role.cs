using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Role
    {
        public int Id { get; set; }
        public string? RoleName { get; set; }

        // Navigation Property
        public ICollection<User> Users { get; set; }
    }
}
