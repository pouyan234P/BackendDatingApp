using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Dto
{
    public class UserforDetialDTO
    {
        public int ID { get; set; }
        public String Username { get; set; }
        public string Gender { get; set; }
        public int age { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActivited { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Photourl { get; set; }
        public IEnumerable<PhotoforDTO> Photoes { get; set; }
    }
}
