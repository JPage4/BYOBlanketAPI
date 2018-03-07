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
    public class NapSpaceController : Controller
    {
        private BYOBDbContext _context;
        private ILogger _log;

        public NapSpaceController(BYOBDbContext ctx, ILogger<NapSpaceController> logger)
        {
            _context = ctx;
            _log = logger;
        }

        //Get all products
        [HttpGet]
        public IActionResult Get()
        {
            var NapSpace = _context.NapSpace.ToList();
            if (NapSpace == null)
            {
                return NotFound();
            }
            return Ok(NapSpace);
        }

        // GET a single product type
        [HttpGet("{id}", Name = "GetSingleNapSpace")]

        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                NapSpace napSpace = _context.NapSpace.Single(n => n.NapSpaceId == id);

                if (napSpace == null)
                {
                    return NotFound();
                }
                return Ok(napSpace);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound(ex);
            }
        }

/* Sample POST request:
{
    "Title": "Real Big Bed",
    "Description": "You sleep in it.",
    "Price": "$10",
    "Rules": "Don't be weird",
    "Payment": "MasterCard",
    "Address": "444 Whatever St.",
    "PictureURL": "pic.com",
}
*/
        [HttpPost]
        public IActionResult Post([FromBody] NapSpace newNapSpace)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.NapSpace.Add(newNapSpace);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (NapSpaceExists(newNapSpace.NapSpaceId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetSingleNapSpacet", new { id = newNapSpace.NapSpaceId }, newNapSpace);
        }

        private bool NapSpaceExists(int NapSpaceId)
        {
            return _context.NapSpace.Any(g => g.NapSpaceId == NapSpaceId);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult PUT(int id, [FromBody] NapSpace newNapSpace)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != newNapSpace.NapSpaceId)
            {
                return BadRequest();
            }

            _context.NapSpace.Update(newNapSpace);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NapSpaceExists(id))
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

            NapSpace napSpace = _context.NapSpace.Single(m => m.NapSpaceId == id);

            if (napSpace == null)
            {
                return NotFound();
            }

            _context.NapSpace.Remove(napSpace);
            _context.SaveChanges();

            return Ok(napSpace);

        }
    }
}
