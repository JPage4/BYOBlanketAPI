using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BYOBlanketAPI.Models
{
    public class User : IdentityUser
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        [Required]
        public override string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
