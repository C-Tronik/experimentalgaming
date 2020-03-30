﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Slot_api.Models;

namespace Slot_api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserinfoesController : ControllerBase
    {
        private readonly slotContext _context;

        public UserinfoesController(slotContext context)
        {
            _context = context;
        }

        // GET: api/Userinfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Userinfo>>> GetUserinfo()
        {
            return await _context.Userinfo.ToListAsync();
        }

        // GET: api/Userinfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Userinfo>> GetUserinfo(int id)
        {
            var userinfo = await _context.Userinfo.FindAsync(id);

            if (userinfo == null)
            {
                return NotFound();
            }

            return userinfo;
        }

        // PUT: api/Userinfoes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserinfo(int id, Userinfo userinfo)
        {
            if (id != userinfo.UserId)
            {
                return BadRequest();
            }

            _context.Entry(userinfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserinfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Userinfoes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Userinfo>> PostUserinfo(Userinfo userinfo)
        {
            _context.Userinfo.Add(userinfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserinfo", new { id = userinfo.UserId }, userinfo);
        }

        // DELETE: api/Userinfoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Userinfo>> DeleteUserinfo(int id)
        {
            var userinfo = await _context.Userinfo.FindAsync(id);
            if (userinfo == null)
            {
                return NotFound();
            }

            _context.Userinfo.Remove(userinfo);
            await _context.SaveChangesAsync();

            return userinfo;
        }

        private bool UserinfoExists(int id)
        {
            return _context.Userinfo.Any(e => e.UserId == id);
        }
    }
}
