using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BYOBlanketAPI.Data;
using BYOBlanketAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BYOBlanketAPI.Controllers
{
    [Route("api/[controller]")]
    public class NapSpaceController : Controller
    {
        private readonly UserManager<User> _userManager;
        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        private BYOBDbContext _context;
        private ILogger _log;

        public NapSpaceController(BYOBDbContext ctx, ILogger<NapSpaceController> logger, UserManager<User> userManager)
        {
            _context = ctx;
            _log = logger;
            _userManager = userManager;
        }

        //Get all products
        [HttpGet]
        public IActionResult GET()
        {
            var NapSpace = _context.NapSpace.Include("User").ToList();
            if (NapSpace == null)
            {
                return NotFound();
            }
            return Ok(NapSpace);
        }

        // GET a single product type
        [HttpGet("{id}", Name = "GetSingleNapSpace")]

        public IActionResult GET(int id)
        {
            var NapSpace = _context.NapSpace.Include("User").ToList();
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
        [Authorize]
        public async Task<IActionResult> POST([FromBody] NapSpace newNapSpace)
        {
            ModelState.Remove("User");
            User user = await _context.User.Where(u => u.UserName == User.Identity.Name).SingleOrDefaultAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            newNapSpace.User = user;

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

            return CreatedAtRoute("GetSingleNapSpace", new { id = newNapSpace.NapSpaceId }, newNapSpace);
        }

        private bool NapSpaceExists(int NapSpaceId)
        {
            return _context.NapSpace.Any(g => g.NapSpaceId == NapSpaceId);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PUT(int id, [FromBody] NapSpace newNapSpace)
        {
            ModelState.Remove("User");
            User user = await _context.User.Where(u => u.UserName == User.Identity.Name).SingleOrDefaultAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != newNapSpace.NapSpaceId)
            {
                return BadRequest();
            }
            newNapSpace.User = user;

            _context.NapSpace.Update(newNapSpace);

            try
            {
               await _context.SaveChangesAsync();
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

            return new StatusCodeResult(StatusCodes.Status200OK);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DELETE(int id)
        {
            ModelState.Remove("User");
            User user = await _context.User.Where(u => u.UserName == User.Identity.Name).SingleOrDefaultAsync();

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
