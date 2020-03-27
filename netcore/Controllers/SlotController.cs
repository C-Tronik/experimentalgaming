using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Api_slot_machine.Models;

namespace Api_slot_machine.Controllers
{
    [Route("api/[controller]")]
    public class SlotController : ControllerBase
    {
        public SlotController(Appdb db)
        {
            Db = db;
        }

        // GET api/slot/
        [HttpGet]
        public  int Estraction()
        {
            int[] Numeri = new int[] { 0, 1, 1, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4 };
            var casuale = new Random();
            return Numeri[casuale.Next(Numeri.Length)];

        }

        // GET api/slot/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new SlotQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // POST api/slot  ----> aggiungere autenticazione 
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Slot_model_utenti body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            await body.InsertAsync();
            return new OkObjectResult(body);
        }

        // PUT api/slot/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody]Slot_model_utenti body)
        {
            await Db.Connection.OpenAsync();
            var query = new SlotQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.name = body.name;
            result.e_mail = body.e_mail;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        // DELETE api/slot/5 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new SlotQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            await result.DeleteAsync();
            return new OkResult();
        }

        // DELETE api/slot
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            await Db.Connection.OpenAsync();
            var query = new SlotQuery(Db);
            await query.DeleteAllAsync();
            return new OkResult();
        }

        public Appdb Db { get; }
    }
}
