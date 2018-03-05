﻿using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BYOBlanketAPI.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace BYOBlanketAPI.Data
{
    public static class DbInitializer
    {
        public async static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<BYOBDbContext>();
            context.Database.EnsureCreated();
            
                var roleStore = new RoleStore<IdentityRole>(context);
                var userstore = new UserStore<User>(context);
                User user = new User
                {
                    FirstName = "Jesse",
                    LastName = "Page",
                    UserName = "jesse@buttz.com",
                    NormalizedUserName = "JESSE@BUTTZ.COM",
                    Email = "jesse@buttz.com",
                    NormalizedEmail = "JESSE@BUTTZ.COM",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                //if (!context.Roles.Any(r => r.Name == "Administrator"))
                //{
                //    var role = new IdentityRole { Name = "Administrator", NormalizedName = "Administrator" };
                //    await roleStore.CreateAsync(role);
                //}

                if (!context.User.Any(u => u.FirstName == "Jesse"))
                {
                    //  This method will be called after migrating to the latest version.
                    //User user = new User
                    //{
                    //    FirstName = "Jesse",
                    //    LastName = "Page",
                    //    UserName = "jesse@buttz.com",
                    //    NormalizedUserName = "JESSE@BUTTZ.COM",
                    //    Email = "jesse@buttz.com",
                    //    NormalizedEmail = "JESSE@BUTTZ.COM",
                    //    EmailConfirmed = true,
                    //    LockoutEnabled = false,
                    //    SecurityStamp = Guid.NewGuid().ToString("D")
                    //};
                    var passwordHash = new PasswordHasher<User>();
                    user.PasswordHash = passwordHash.HashPassword(user, "jjjjjj");
                    await userstore.CreateAsync(user);
                    await userstore.AddToRoleAsync(user, "Administrator");
                }

                // Look for any products.
                if (context.NapSpace.Any())
                {
                    return;   // DB has been seeded
                }

                var napSpace = new NapSpace[]
                {
                    new NapSpace {
                        Title = "Big Comfy Couch",
                        Description = "Oh man this couch will eat you",
                        Price = "$5",
                        Rules = "Please don't drool on the pillows",
                        Payment = "PayPal",
                        Address = "123 Buttz Lane",
                        PictureURL = "pic.com"
                        //OwnerId = 1,
                        //OwnerEmail = "jesse@buttz.com"
                    },
                    new NapSpace {
                        Title = "Tent",
                        Description = "Sick ass tent",
                        Price = "$3.50",
                        Rules = "The nicest tent of all time. It's for glamping.",
                        Payment = "Cash Money",
                        Address = "1111 Yep Rd",
                        PictureURL = "pic.com"
                        //OwnerId = 1,
                        //OwnerEmail = "jesse@buttz.com"
                    }
                };

                foreach (NapSpace n in napSpace)
                {
                    context.NapSpace.Add(n);
                }
                context.SaveChanges();

                var reservation = new Reservation[]
                {
                    new Reservation {
                        NapSpaceTitle = "Tent",
                        CalendarColor = "#5f6dd0",
                        StartDateTime = new DateTime(2018, 3, 15, 12, 0, 0),
                        EndDateTime = new DateTime(2018, 3, 15, 1, 0, 0),
                        NapSpaceId = 2,
                        User = user
                    },
                   new Reservation
                   {
                       NapSpaceTitle = "Big Comfy Couch",
                       CalendarColor = "#5f6dd0",
                       StartDateTime = new DateTime(2018, 3, 15, 11, 0, 0),
                       EndDateTime = new DateTime(2018, 3, 15, 12, 0, 0),
                       NapSpaceId = context.NapSpace.Single(n => n.Title == "Big Comfy Couch").NapSpaceId,
                       User = user
                   }
                };

                foreach (Reservation r in reservation)
                {
                    context.Reservation.Add(r);
                }
                context.SaveChanges();
   
        }
    }
}