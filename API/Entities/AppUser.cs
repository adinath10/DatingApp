using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;

namespace API.Entities
{
    // Entities is for method return type
    // Entity is abstraction of physical thing.
    // In our case user is physical thing
    // It contains user related properties
    public class AppUser
    {
        public int Id { get; set; } //Property name should be in titlecase 
        public string UserName { get; set; } //using getter setter shorthand property
        public byte[] PasswordHash { get; set; } //List is returned when we calculate the hash
        public byte[] PasswordSalt { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
        public string Gender { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<UserLike> LikedByUsers { get; set; }
        public ICollection<UserLike> LikedUsers { get; set; }
    }
}