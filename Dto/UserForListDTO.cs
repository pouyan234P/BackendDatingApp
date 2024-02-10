using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Dto
{
    public class UserForListDTO
    {
        public int ID { get; set; }
        public String Username { get; set; }
        public string Gender { get; set; }
        public int age { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActivited { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string photourl { get; set; }
    }
}
