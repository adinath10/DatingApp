using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    // Entity is abstraction of physical thing.
    // In our case user is physical thing
    // It contains user related properties
    public class AppUser
    {
        public int Id { get; set; } //Property name should be in titlecase 

        public string UserName { get; set; } //using getter setter shorthand property
    }
}