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
    public class PeriodManagementController : ControllerBase
    {
        private readonly PeriodManagementRepository repo = new PeriodManagementRepository();

        // GET api/periodmanagement
        [HttpGet]
        public IActionResult Get()
        {
            var q = repo.GetPeriodManagements();
            return Ok(q);
        }

        // // GET api/periodmanagement/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var q = repo.GetPeriodManagement(id);
            if (q == null)
            {
                return NotFound();
            }
            return Ok(q);
        }


        // GET api/periodmanagement/getbyyear/[year]
        [HttpGet]
        [Route("getbyyear/{year}")]
        public IActionResult GetByYear(int year)
        {
            var q = repo.GetPeriodManagements(year);
            return Ok(q);
        }

        [HttpPost]
        [Route("startperiod/{id}")]
        public IActionResult StartPeriod(Guid id)
        {
            var q = repo.UpdateActive(id, true);
            return Ok(new ApiOkResponse(q));
        }

        [HttpPost]
        [Route("edittime")]
        public IActionResult EditTime([FromQuery] Guid id,[FromQuery] DateTime start,[FromQuery] DateTime end)
        {
            var res = repo.EditTime(id, start, end);
            if (res.Item1 == false)
            {
                return new BadRequestResult();
            }

            return Ok(new ApiOkResponse(res.Item3));
        }



        // POST api/periodmanagement
        [HttpPost]
        public IActionResult Post([FromBody] PeriodManagement value)
        {
            var q = repo.AddPeriodManagement(value);

            // failed insert
            if (!q.Item1 || !ModelState.IsValid)
            {
                return BadRequest(new BadRequestObjectResult(this.ModelState));
            }

            return Ok(new ApiOkResponse(q.Item3));
        }

        // PUT api/periodmanagement/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] PeriodManagement value)
        {
            var res = repo.UpdatePeriodManagement(value);
            if (res.Item1 == false)
            {
                return BadRequest(new ApiBadRequestResponse(this.ModelState));
            }

            return Ok(new ApiOkResponse(res.Item3));
        }

        // DELETE api/periodmanagement/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            var res = repo.DeletePeriodManagement(id);
        }
    }
}
