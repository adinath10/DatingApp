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
        public DbSet<UserLike> Likes { get; set; }
         
        // We can configure a one-to-many relationship using Fluent API by 
        //overriding the OnModelCreating method in the context class, 
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // HasKey method is used to denote the property that uniquely identifies an entity (the EntityKey),
            // and which is mapped to the Primary Key field
            builder.Entity<UserLike>()
                .HasKey(k => new {k.SourceUserId, k.LikedUserId});

            // configures one-to-many relationship

            // start configuring with any one entity class. 
            // So, builder.Entity<UserLike>() starts with the UserLike entity.
            builder.Entity<UserLike>()
            // .HasOne(s => s.SourceUser) specifies that the UserLike entity has one SourceUser property.
                .HasOne(s => s.SourceUser)
            // .WithMany(l => l.LikedUsers) specifies that the UserLike entity class includes many LikedUsers entities
                .WithMany(l => l.LikedUsers)
            // .HasForeignKey<int>(s => s.SourceUserId); specifies the foreign key property in the UserLike entity.
                .HasForeignKey(s => s.SourceUserId)
            // hence deleting a SourceUser will delete the related LikedUsers.
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserLike>()
                .HasOne(s => s.LikedUser)
                .WithMany(l => l.LikedByUsers)
                .HasForeignKey(s => s.LikedUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}