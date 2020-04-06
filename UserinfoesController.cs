using System;
using System.Collections.Generic;
using System.Linq;

using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Slot_api.Models;
using Slot_api.estrazione.egypt;

namespace Slot_api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserinfoesController : ControllerBase
    {
        private readonly slotContext _context;
        

        

        

        public UserinfoesController(slotContext context )
        {
            _context = context;
            
        }

        // GET: api/Userinfoes
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Userinfo>>> GetUserinfo()
        {
            return await _context.Userinfo.ToListAsync();
        }
        [Authorize]
        [HttpGet("/userinfoes/sp/{spin}/{id}")]
        public async Task<IActionResult> Spin(int spin,int id)
        {
            var userinfo = await _context.Userinfo.FindAsync(id);
            if (CheckEmailToken(userinfo))
            {
               
                userinfo.Score -= spin;
                _context.Entry(userinfo).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                Egypt_slot egypt_slot = new Egypt_slot();
               
                Egypt_slot_estraction egypt_estraction = egypt_slot.Estrazione_Egypt_Slot();

                return Ok(new { userinfo.Score , egypt_estraction  });
            }
            else
                return Conflict();
        }

        // GET: api/Userinfoes/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserinfo(int id)
        {
            

            var userinfo = await _context.Userinfo.FindAsync(id) ;


            if (userinfo == null)
            {
                return NotFound();
            }
            else if (CheckEmailToken(userinfo))
                return Ok(userinfo.UserId+userinfo.Username+userinfo.Score);
            else return BadRequest();
        }

        // PUT: api/Userinfoes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserinfo(int id, Userinfo userinfo)
        {
          
            if (id != userinfo.UserId)
            {
                return BadRequest();
            }
            else if (CheckEmailToken(userinfo)) { 
                _context.Entry(userinfo).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            try
            {
                Accepted();
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
            if (UserEmailExists(userinfo.Email))
            {
                return Conflict();
            } 
           
            userinfo.Password=Sha256_hash(userinfo.Password);
            _context.Userinfo.Add(userinfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserinfo", new { id = userinfo.UserId }, userinfo);
        }

        // DELETE: api/Userinfoes/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Userinfo>> DeleteUserinfo(int id)
        {
           
            var userinfo = await _context.Userinfo.FindAsync(id);
            if (userinfo == null)
            {
                return NotFound();
            }
            else if (CheckEmailToken(userinfo))
            {
                _context.Userinfo.Remove(userinfo);
                await _context.SaveChangesAsync();
                return userinfo;
            }
            else return NotFound();

            
        }



        [Authorize(Roles = "admin")]
        [HttpDelete]
        
        public async Task<ActionResult<Userinfo>> DeleteAllUser()
        {
            var userinfo =  _context.Userinfo.ToList();
            _context.Userinfo.RemoveRange(userinfo);
            await _context.SaveChangesAsync();
            return Accepted();
        }

        private bool UserinfoExists(int id)
        {
            return _context.Userinfo.Any(e => e.UserId == id);
        }

        private bool UserEmailExists(string email)
        {
            return _context.Userinfo.Any(e => e.Email == email);
        }

        public static String Sha256_hash(string value)
        {
            StringBuilder Sb = new StringBuilder();

            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        private bool CheckEmailToken(Userinfo userinfo)
        {
            var email = User.FindFirst("Email")?.Value;
            if (userinfo.Email == email)
                return true;
            else return false;
                
        }
    }
}
