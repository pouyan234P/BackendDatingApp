using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly Datacontext _datacontext;

        public UserRepository(Datacontext datacontext)
        {
            _datacontext = datacontext;
        }
        public void add<T>(T entity) where T : class
        {
            _datacontext.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _datacontext.Remove(entity);
        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
            var users =await  _datacontext.Users.Include(t => t.Photos).Select(t => t).ToListAsync();
            return users;
        }

        public async Task<User> GetUser(int ID)
        {
         var user= await _datacontext.Users.Include(t=>t.Photos).FirstOrDefaultAsync(t=>t.ID ==ID);
            return user;
        }

        public async Task<bool> SaveAll()
        {

            return await _datacontext.SaveChangesAsync()>0;
        }

        public async Task<bool> UserExist(int ID)
        {
            var user =await  _datacontext.Users.Where(x => x.ID == ID).Select(t => t).FirstOrDefaultAsync();
            if (user != null)
            {
                return true;
            }
            return false;
        }
    }
}
