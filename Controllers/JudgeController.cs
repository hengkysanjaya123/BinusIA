using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Backend.Model;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JudgeController : ControllerBase
    {
        BinusIAContext db;

        public JudgeController(BinusIAContext _db)
        {
            this.db = _db;
        }

        // GET api/judge
        [HttpGet]
        public IActionResult Get()
        {
            var q = db.UserHeader
                    .Where(x => x.Role == 2)
                    .OrderBy(x => x.Name).ToList();

            q = FormatDisplay(q);

            return Ok(q);
        }

        private List<UserHeader> FormatDisplay(List<UserHeader> userHeaders)
        {
            var q = userHeaders.Select(x => new UserHeader
            {
                BinusianId = x.BinusianId,
                Email = x.Email,
                Name = x.Name,
                Position = x.Position,
                Department = x.Department,
                BusinessUnit = x.BusinessUnit,
                Extension = x.Extension,
                Handphone = x.Handphone,
                JobBand = x.JobBand,
                LeadId = x.LeadId,
                Role = x.Role,
                Lead = x.Lead == null ? null : new UserHeader()
                {
                    BinusianId = x.Lead.BinusianId,
                    Email = x.Lead.Email,
                    Name = x.Lead.Name,
                    Position = x.Lead.Position,
                    Department = x.Lead.Department,
                    BusinessUnit = x.Lead.BusinessUnit,
                    Extension = x.Lead.Extension,
                    Handphone = x.Lead.Handphone,
                    JobBand = x.Lead.JobBand,
                    LeadId = x.Lead.LeadId,
                    Role = x.Lead.Role
                }
            }).ToList();
            return q;
        }

        // GET api/judge/5
        [HttpGet("{binusianId}")]
        public IActionResult Get(string binusianId)
        {
            System.Console.WriteLine("myid : ", binusianId);

            var q = db.UserHeader
                    .Where(x => x.Role == 2
                            && x.BinusianId == binusianId)   
                    .ToList();

            q = FormatDisplay(q);
            
            return Ok(q);
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
