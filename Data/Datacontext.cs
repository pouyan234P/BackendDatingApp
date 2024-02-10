using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class Datacontext: DbContext
    {
        public Datacontext(DbContextOptions<Datacontext> options): base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> photos { get; set; }
    }
}
