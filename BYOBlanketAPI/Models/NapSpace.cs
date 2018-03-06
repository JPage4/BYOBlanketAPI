using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BYOBlanketAPI.Models
{
    public class NapSpace
    {
        [Key]
        public int NapSpaceId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Price { get; set; }
        [Required]
        public string Rules { get; set; }
        [Required]
        public string Payment { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PictureURL { get; set; }

        [Required]
        public User User {get; set;}
        [Required]
        public int UserId {get; set;}
        [Required]
        public string UserEmail {get; set;}
    }
}
