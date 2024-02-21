using Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Concrete
{
    public class RoleDto : IDto
    {
        public int Id { get; set; }
        public string RoleName { get; set; }

        // Navigation Property
        public ICollection<UserDto>? Users { get; set; }
    }
}
