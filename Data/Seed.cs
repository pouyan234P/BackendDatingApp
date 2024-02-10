using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class Seed
    {
        public static void SeedUser(Datacontext datacontext)
        {
            if(!datacontext.Users.Any())
            {
                var UserData = System.IO.File.ReadAllText("Data/UserSeed.json");
                var user = JsonConvert.DeserializeObject<List<User>>(UserData);
                foreach (var Users in user)
                {
                    byte[] PasswordHash, PasswordSalt;
                    CreatePasswordHash("password", out PasswordHash, out PasswordSalt);
                    Users.Password = PasswordHash;
                    Users.PasswordSalt = PasswordSalt;
                    Users.Username = Users.Username.ToLower();
                    datacontext.Users.Add(Users);
                    
                }
                datacontext.SaveChanges();
            }
        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmc = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmc.Key;
                passwordHash = hmc.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
