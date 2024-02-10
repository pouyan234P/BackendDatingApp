using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
   public interface IUserRepository
    {
        void add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<bool> UserExist(int ID);
        Task<IEnumerable<User>> GetAllUser();
        Task<User> GetUser(int ID);
    }
}
