using Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Log : IEntity
    {
        public int Id { get; set; }
        public string? LogMessage { get; set; }
        public DateTime LogDate { get; set; }
        public int UserId { get; set; }

        // Navigation Property
        public User? User { get; set; }
    }
}
