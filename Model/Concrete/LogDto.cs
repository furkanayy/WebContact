using Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Concrete
{
    public class LogDto : IDto
    {
        public int Id { get; set; }
        public string? LogMessage { get; set; }
        public DateTime LogDate { get; set; }
        public int UserId { get; set; }

        // Navigation Property
        public UserDto? User { get; set; }
    }
}
