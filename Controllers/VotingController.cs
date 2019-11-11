using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Backend.Model;
using Backend.Repositories;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateModel]
    public class VotingController : ControllerBase
    {
        private readonly VotingRepository repo = new VotingRepository();

        // GET api/voting
        [HttpGet]
        public IActionResult Get()
        {
            var q = repo.GetVotings();
            return Ok(q);
        }

        // // GET api/voting/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var q = repo.GetVoting(id);
            if(q == null){
                return NotFound();
            }
            return Ok(q);
        }

        // // POST api/voting
        [HttpPost]
        public IActionResult Post([FromBody] Voting value)
        {
            var q = repo.AddVoting(value);

            // failed insert
            if(!q.Item1 || !ModelState.IsValid){
                return BadRequest(new BadRequestObjectResult(this.ModelState));
            }
            
            return Ok( new ApiOkResponse(q.Item3));
        }

        // PUT api/voting/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Voting value)
        {
            var res = repo.UpdateVoting(value);
            if(res.Item1 == false){
                return BadRequest(new ApiBadRequestResponse(this.ModelState));
            }

            return Ok(new ApiOkResponse(res.Item3));
        }

        // DELETE api/voting/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            var res = repo.DeleteVoting(id);
        }
    }
}
