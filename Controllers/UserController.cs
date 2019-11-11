using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Backend.Model;
using Backend.Repositories;
using Microsoft.EntityFrameworkCore;
using Backend.ModelDTO;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateModel]
    public class UserController : ControllerBase
    {
        BinusIAContext db = new BinusIAContext();

        // GET api/user
        [HttpGet]
        public IActionResult Get(int page = 1, int pageCount = 20)
        {
            string href = DTOHelper.GetHref(Request);

            var q = db.UserHeader.ToList();

            var userHeaders = FormatDisplay(q, href);

            userHeaders = Paginate(page, pageCount, href, userHeaders);

            return Ok(userHeaders);
        }

        private List<UserHeader> Paginate(int page, int pageCount, string href, List<UserHeader> userHeaders)
        {
            int totalcount = userHeaders.Count();
            int totalpage = ((totalcount + pageCount - 1) / pageCount);
            userHeaders = userHeaders.Skip(pageCount * (page - 1)).Take(pageCount).ToList();
            Response.Headers.Add("X-Pagination-Total-Count", totalcount.ToString());
            Response.Headers.Add("X-Pagination-Page-Count", totalpage.ToString());
            Response.Headers.Add("X-Pagination-Current-Page", page.ToString());
            Response.Headers.Add("X-Pagination-Per-Page", pageCount.ToString());
            List<string> links = new List<string>
            {
                string.Format("<{0}?page={1}>; rel=first", href, 1),
                string.Format("<{0}?page={1}>; rel=previous", href, page - 1),
                string.Format("<{0}?page={1}>; rel=self", href, page),
                string.Format("<{0}?page={1}>; rel=next", href, page + 1),
                string.Format("<{0}?page={1}>; rel=last", href, totalpage)
            };
            Response.Headers.Add("Link", string.Join(", ", links));
            return userHeaders;
        }

        private List<UserHeader> FormatDisplay(List<UserHeader> userHeaders, string href, bool excludeID = false)
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
                RoleName = x.Role == 1 ? "Committee" : x.Role == 2 ? "Jury" : x.Role == 3 ? "Participant" : "-",
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

            System.Console.WriteLine("List User Header : ", q);
            return q;
        }


        // GET api/user/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            string href = DTOHelper.GetHref(Request);

            var q = db.UserHeader
                .Where(x => x.BinusianId == id)
                .ToList();

            var q2 = FormatDisplay(q, href, true).FirstOrDefault();

            if (q2 == null)
            {
                return NotFound();
            }
            return Ok(q2);
        }

        // GET api/user/
        [HttpGet]
        [Route("filter")]
        public IActionResult Filter([FromQuery] string keyword = "", [FromQuery] int page = 1, [FromQuery] int pageCount = 20)
        {
            string href = DTOHelper.GetHref(Request);

            var q = db.UserHeader
                .Where(x => x.BinusianId.Contains(keyword) ||
                            x.Email.Contains(keyword) ||
                            x.Name.Contains(keyword) ||
                            x.Position.Contains(keyword) ||
                            x.Department.Contains(keyword) ||
                            x.JobBand.Contains(keyword) ||
                            x.LeadId.Contains(keyword) ||
                            x.Role.ToString() == keyword
                )
                .ToList();

            var q2 = FormatDisplay(q, href);
            q2 = Paginate(page, pageCount, href, q2);

            if (q2 == null)
            {
                return NotFound();
            }
            return Ok(q2);
        }

        // POST api/user
        [HttpPost]
        public IActionResult Post([FromBody] UserHeader value)
        {
            try
            {
                var q = db.UserHeader.Where(x => x.BinusianId == value.BinusianId).Count();
                if (q > 0)
                {
                    return BadRequest("BinusianId already exists");
                }
                db.Add(value);
                db.SaveChanges();

                return Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/user
        [HttpPut]
        public IActionResult Put([FromQuery] string oldBinusianId, [FromBody] UserHeader value)
        {
            try
            {
                string href = DTOHelper.GetHref(Request);

                var q = db.UserHeader.Where(x => x.BinusianId == oldBinusianId).FirstOrDefault();
                q.BinusianId = value.BinusianId;
                q.Email = value.Email;
                q.Name = value.Name;
                q.Position = value.Position;
                q.Department = value.Department;
                q.BusinessUnit = value.BusinessUnit;
                q.Extension = value.Extension;
                q.Handphone = value.Handphone;
                q.JobBand = value.JobBand;
                q.LeadId = value.LeadId;
                q.Role = value.Role;

                db.SaveChanges();

                var q2 = new List<UserHeader>();
                q2.Add(q);

                q2 = FormatDisplay(q2, href);

                return Ok(q2.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/user/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            var q = db.UserHeader.Where(x => x.BinusianId == id).FirstOrDefault();
            db.Remove(q);
            db.SaveChanges();
        }

        [HttpGet]
        [Route("getjobbands")]
        public IActionResult GetJobBans()
        {
            var q = db.UserHeader.Select(x => x.JobBand)
                    .Where(x => x != "")
                    .Distinct().ToList();
            return Ok(q);
        }

        [HttpGet]
        [Route("getroles")]
        public IActionResult GetRoles()
        {
            var q = new List<string>()
            {
                "Committee", "Jury", "Participant"
            };
            return Ok(q);
        }
    }
}
