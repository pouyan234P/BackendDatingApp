using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class DataRepository : IDataRepository
    {
        private readonly Datacontext _datacontext;

        public DataRepository(Datacontext datacontext)
        {
            _datacontext = datacontext;
        }
        public async Task<User> Login(string Username, string password)
        {
            var user = await _datacontext.Users.Where(t => t.Username == Username).Select(t => t).FirstOrDefaultAsync();
            if (user == null)
                return null;
            if(!VerfiyPasswordHash(password,user.Password,user.PasswordSalt))
            return null;
            return user;
        }

        private bool VerfiyPasswordHash(string password1, byte[] password2, byte[] passwordSalt)
        {
            using (var hmc = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computehash = hmc.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password1));
                for (int i = 0; i < computehash.Length; i++)
                {
                    if (computehash[i] != password2[i])
                        return false;
                }
                    }
            return true;
        }

        public async Task<User> Register(User user, string Password)
        {
            byte[] PasswordHash, PasswordSalt;
            CreatePasswordHash(Password,out PasswordHash,out PasswordSalt);
            user.Password = PasswordHash;
            user.PasswordSalt = PasswordSalt;
           await _datacontext.Users.AddAsync(user);
            await _datacontext.SaveChangesAsync();
            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmc = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmc.Key;
                passwordHash = hmc.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExist(string Username)
        {
            if (await _datacontext.Users.AnyAsync(x => x.Username == Username))
                return true;
            return false;
        }
    }
}
