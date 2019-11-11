using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Backend.Model;
using Microsoft.EntityFrameworkCore;
using Backend.ModelDTO;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        BinusIAContext db;

        public ReportController(BinusIAContext _db)
        {
            this.db = _db;
        }

        // GET api/report
        [HttpGet]
        public IActionResult Get()
        {
            var q = db.TeamDetail
                    .Include(x => x.Team)
                    .Include(x => x.Team.Proposal)
                        .ThenInclude(y => y.Status)
                    .ToList();

            var q2 = FormatDisplay(q);


            return Ok(q2);
        }

        private List<ReportDTO> FormatDisplay(List<TeamDetail> teamDetails, bool filterDate = false, int year = 0)
        {
            var q = teamDetails
                    .Where(x => x.Team != null
                            && x.Team.Proposal != null)
                    .Select(x => new
                    {
                        props = x.Team.Proposal,
                        td = x
                    })
                    .Select(x => new
                    {
                        obj = x.props
                            .Where(y => y.SubmitDate.Year == year || filterDate == false)
                            .Select(y => new ReportDTO
                            {
                                TeamName = x.td.Team.Name,
                                ProposalTitle = y.ProposalTitle,
                                Category = y.Category,
                                Phase = y.Phase,
                                Status = y.Status.Name,
                                Name = db.UserHeader.Where(z => z.Email == x.td.EmailMember).FirstOrDefault() == null ? "--" : db.UserHeader.Where(z => z.Email == x.td.EmailMember).FirstOrDefault().Name,
                                Role = x.td.Role,
                                Department = db.UserHeader.Where(z => z.Email == x.td.EmailMember).FirstOrDefault() == null ? "--" : db.UserHeader.Where(z => z.Email == x.td.EmailMember).FirstOrDefault().Department,
                                BusinessUnit = db.UserHeader.Where(z => z.Email == x.td.EmailMember).FirstOrDefault() == null ? "--" : db.UserHeader.Where(z => z.Email == x.td.EmailMember).FirstOrDefault().BusinessUnit
                            })
                    })
                    .SelectMany(x => x.obj)
                    .ToList();

            return q;
        }


        // GET api/report/filter
        [HttpGet]
        [Route("filter")]
        public IActionResult Filter(int year)
        {
            var q = db.TeamDetail
                    .Include(x => x.Team)
                    .Include(x => x.Team.Proposal)
                        .ThenInclude(y => y.Status)
                    .ToList();

            var q2 = FormatDisplay(q, true, year);

            return Ok(q2);
        }



        // // GET api/judge/5
        // [HttpGet("{binusianId}")]
        // public IActionResult Get(string binusianId)
        // {
        //     System.Console.WriteLine("myid : ", binusianId);

        //     var q = db.UserHeader
        //             .Where(x => x.Role == 2
        //                     && x.BinusianId == binusianId)
        //             .ToList();

        //     q = FormatDisplay(q);

        //     return Ok(q);
        // }

        // // POST api/values
        // [HttpPost]
        // public void Post([FromBody] string value)
        // {

        // }

        // // PUT api/values/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody] string value)
        // {
        // }

        // // DELETE api/values/5
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}
