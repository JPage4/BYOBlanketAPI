﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BYOBlanketAPI.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public virtual ICollection<NapSpace> NapSpaces
        {
            get; set;
        }

        public virtual ICollection<Reservation> Reservations
        {
            get; set;
        }
    }
}
