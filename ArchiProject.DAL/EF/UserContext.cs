using ArchiProject.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArchiProject.DAL.EF
{
     public class UserContext : DbContext
    {

        public DbSet<User> Users { get; set; }


        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {

        }

    }
}
