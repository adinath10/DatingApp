using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

// DTO stands for Data Transfer objects
namespace API.DTOs
{
    // DTO is for request object
    // single object with multiple properties for reusing the code
    public class RegisterDto
    {
        [Required] public string Username { get; set; }
        [Required] public string KnownAs { get; set; }
        [Required] public string Gender { get; set; }
        [Required] public DateTime DateOfBirth { get; set; }
        [Required] public string City { get; set; }
        [Required] public string Country { get; set; }
        
        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string Password { get; set; }
    }
}