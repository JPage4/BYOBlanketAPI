using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BYOBlanketAPI.Data;
using BYOBlanketAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BYOBlanketAPI.Controllers
{
    [Route("api/[controller]")]
    public class ReservationController : Controller
    {
        private BYOBDbContext _context;
        private ILogger _log;

        public ReservationController(BYOBDbContext ctx, ILogger<ReservationController> logger)
        {
            _context = ctx;
            _log = logger;
        }

        //Get all product types
        [HttpGet]
        public IActionResult Get()
        {
            //
            var Reservation = _context.Reservation.ToList();
            if (Reservation == null)
            {
                return NotFound();
            }
            return Ok(Reservation);
        }

        // GET a single product type
        [HttpGet("{id}", Name = "GetSingleReservation")]

        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Reservation reservation = _context.Reservation.Single(n => n.ReservationId == id);

                if (reservation == null)
                {
                    return NotFound();
                }
                return Ok(reservation);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound(ex);
            }
        }

/* Sample POST request:
{
    "NapSpaceTitle": "Real Big Bed",
    "CalendarColor": "#5f6dd0",
    "StartDateTime": "new DateTime(2018, 3, 15, 12, 0, 0)",
    "EndDateTime": "new DateTime(2018, 3, 15, 1, 0, 0)",
    "NapSpaceId": ?,
    "User": ?
}
*/
        [HttpPost]
        public IActionResult Post([FromBody] Reservation newReservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Reservation.Add(newReservation);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ReservationExists(newReservation.ReservationId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetSingleNapSpace", new { id = newReservation.ReservationId }, newReservation);
        }

        private bool ReservationExists(int ReservationId)
        {
            return _context.Reservation.Any(g => g.ReservationId == ReservationId);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult PUT(int id, [FromBody] Reservation newReservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != newReservation.ReservationId)
            {
                return BadRequest();
            }

            _context.Reservation.Update(newReservation);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult DELETE(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Reservation reservation = _context.Reservation.Single(m => m.ReservationId == id);

            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservation.Remove(reservation);
            _context.SaveChanges();

            return Ok(reservation);

        }
    }
}