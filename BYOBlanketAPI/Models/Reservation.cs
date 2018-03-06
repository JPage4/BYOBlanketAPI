using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BYOBlanketAPI.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        [Required]
        public string NapSpaceTitle { get; set; }
        [Required]
        public string CalendarColor { get; set; }
        [Required]
        public DateTime StartDateTime { get; set; }
        [Required]
        public DateTime EndDateTime { get; set; }

        [Required]
        public int NapSpaceId { get; set; }
        public NapSpace NapSpace { get; set; }

        [Required]
        public User User {get; set;}
        [Required]
        public int UserId {get; set;}
        [Required]
        public string UserEmail {get; set;}
    }
}
