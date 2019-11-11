using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Backend.Model;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        BinusIAContext db;

        public ValuesController(BinusIAContext _db){
            this.db = _db;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Voting>> Get()
        {
            var q = db.Voting.ToList();
            return q;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Voting> Get(Guid id)
        {
            var q = db.Voting.Where(x=> x.VotingId == id).FirstOrDefault();
            return q;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
