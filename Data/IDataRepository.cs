using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public interface IDataRepository
    {
        public Task<User> Register(User user,string Password);
        public Task<User> Login(string Username, string password);
        public Task<bool> UserExist(string Username);
    }
}
