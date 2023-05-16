using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    // An instance of DbContext represents a session with the database which can be used to query and 
    // save instances of your entities to a database.
    public class DataContext : DbContext
    {
        // To use DbContext in our application, we need to create the class that derives from DbContext, 
        // also known as context class.
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        // contains the properties of AppUser
        // Creates a DbSet<TEntity> that can be used to query and save instances of TEntity
        public DbSet<AppUser> Users { get; set; }
    }
}