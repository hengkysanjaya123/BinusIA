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
    public class ProposalController : ControllerBase
    {
        BinusIAContext db = new BinusIAContext();

        // GET api/proposal
        [HttpGet]
        public IActionResult Get(int page = 1, int pageCount = 20)
        {
            string href = DTOHelper.GetHref(Request);

            var q = db.Proposal.ToList();

            var proposalDTOs = FormatDisplay(q, href);

            proposalDTOs = Paginate(page, pageCount, href, proposalDTOs);

            return Ok(proposalDTOs);
        }

        private List<ProposalDTO> Paginate(int page, int pageCount, string href, List<ProposalDTO> proposalDTOs)
        {
            int totalcount = proposalDTOs.Count();
            int totalpage = ((totalcount + pageCount - 1) / pageCount);
            proposalDTOs = proposalDTOs.Skip(pageCount * (page - 1)).Take(pageCount).ToList();
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
            return proposalDTOs;
        }

        private List<ProposalDTO> FormatDisplay(List<Proposal> proposals, string href, bool excludeID = false)
        {
            var q = proposals.Select(x => new
            {
                obj = x,
                Total = x.JuryDetail.Count == 0 ? 0 : x.JuryDetail.Sum(y =>
                                y.ScoringDetail.Count == 0 ? 0 :
                               y.ScoringDetail.Sum(z => (z.Weight * z.Score) / 100)
                          ),
                nJury = x.JuryDetail.Count
            })
            .Select(x => new ProposalDTO()
            {
                Value = new Proposal()
                {
                    ProposalId = x.obj.ProposalId,
                    Phase = x.obj.Phase,
                    StatusId = x.obj.StatusId,
                    Status = new StatusProposal()
                    {
                        StatusId = x.obj.StatusId,
                        Name = x.obj.Status.Name
                    },
                    ProposalTitle = x.obj.ProposalTitle,
                    Category = x.obj.Category,
                    TeamId = x.obj.TeamId,
                    Team = new TeamHeader()
                    {
                        TeamId = x.obj.TeamId,
                        Name = x.obj.Team.Name,
                        Date = x.obj.Team.Date,
                        Photo = x.obj.Team.Photo,
                        TeamDetail = x.obj.Team.TeamDetail
                    },
                    SubmitDate = x.obj.SubmitDate,
                    Summary = x.obj.Summary,
                    Keyword = x.obj.Keyword,
                    CreatedBy = x.obj.CreatedBy,
                    ApprovalDate = x.obj.ApprovalDate,
                    FinalizedDate = x.obj.FinalizedDate,
                    FinalizedBy = x.obj.FinalizedBy,
                    Order = x.obj.Order,
                    Score = x.nJury == 0 ? 0 : x.Total / x.nJury,
                    JuryDetail = x.obj.JuryDetail.Select(y => new JuryDetail
                    {
                        JuryDetailId = y.JuryDetailId,
                        ProposalId = y.ProposalId,
                        Phase = y.Phase,
                        EmailJury = y.EmailJury,
                        ApproveScore = y.ApproveScore,
                        ApprovalCode = y.ApprovalCode,
                        ApprovalDate = y.ApprovalDate,
                        AssignDate = y.AssignDate,
                        ScoringDetail = y.ScoringDetail
                    }).ToList(),
                },
                Links = LinkGenerator.GenerateLink(href, x.obj.ProposalId, excludeID)
            })
            .ToList();

            System.Console.WriteLine("List ProposalDTO : ", q);
            return q;
        }


        // GET api/proposal/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            string href = DTOHelper.GetHref(Request);

            var q = db.Proposal
                .Where(x => x.ProposalId == id)
                .ToList();

            var q2 = FormatDisplay(q, href, true).FirstOrDefault();

            if (q2 == null)
            {
                return NotFound();
            }
            return Ok(q2);
        }

        // GET api/proposal/
        [HttpGet]
        [Route("filter")]
        public IActionResult Filter([FromQuery] int year, [FromQuery] int phase, [FromQuery] int page = 1, [FromQuery] int pageCount = 20)
        {
            string href = DTOHelper.GetHref(Request);

            var q = db.Proposal
               .Where(x => x.SubmitDate.Year == year
                           && x.Phase == phase)
                .ToList();

            var q2 = FormatDisplay(q, href);
            q2 = Paginate(page, pageCount, href, q2);

            if (q2 == null)
            {
                return NotFound();
            }
            return Ok(q2);
        }

        // POST api/proposal
        [HttpPost]
        public IActionResult Post([FromBody] Proposal value)
        {
            try
            {
                db.Add(value);
                db.SaveChanges();

                return Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/proposal/
        [HttpPut]
        public IActionResult Put([FromBody] Proposal value)
        {
            try
            {
                var q = db.Proposal.Where(x => x.ProposalId == value.ProposalId).FirstOrDefault();
                q.Phase = value.Phase;
                q.StatusId = value.StatusId;
                q.ProposalTitle = value.ProposalTitle;
                q.Category = value.Category;
                q.TeamId = value.TeamId;
                q.SubmitDate = value.SubmitDate;
                q.Summary = value.Summary;
                q.Keyword = value.Keyword;
                q.CreatedBy = value.CreatedBy;
                q.ApprovalDate = value.ApprovalDate;
                q.FinalizedDate = value.FinalizedDate;
                q.FinalizedBy = value.FinalizedBy;
                q.Order = value.Order;

                db.SaveChanges();
                return Ok(q);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/proposal/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            var q = db.Proposal.Where(x => x.ProposalId == id).FirstOrDefault();
            if (q != null)
            {
                var q2 = db.ProposalDetail.Where(x => x.ProposalId == q.ProposalId);
                db.RemoveRange(q2);
                db.Remove(q);
            }
        }


        // function to update the presentation order of specific proposal
        // PUT api/proposal/updateorder
        [HttpPut]
        [Route("updateorder")]
        public IActionResult UpdateOrder(Guid id, int order)
        {
            try
            {
                string href = DTOHelper.GetHref(Request);
                var q = db.Proposal.Where(x => x.ProposalId == id).FirstOrDefault();
                q.Order = order;
                db.SaveChanges();
                return Ok(q);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // function to add judge to specific proposal & phase
        // POST api/proposal/addjudge
        [HttpPost]
        [Route("addjudge")]
        public IActionResult AddJudge(Guid proposalId, int phase, string email)
        {
            var q = db.JuryDetail.Where(x => x.ProposalId == proposalId
                        && x.Phase == phase
                        && x.EmailJury == email).Count();
            if (q > 0)
            {
                return BadRequest("Jury is already in this phase of proposal");
            }

            JuryDetail jd = new JuryDetail()
            {
                JuryDetailId = Guid.NewGuid(),
                ProposalId = proposalId,
                Phase = phase,
                EmailJury = email,
                ApproveScore = 0,
            };
            db.Add(jd);
            db.SaveChanges();

            return Ok(jd);
        }

        // DELETE api/proposal/deletejudge
        [HttpDelete]
        [Route("deletejudge")]
        public IActionResult DeleteJudge(Guid proposalId, int phase, string email)
        {
            try
            {
                var q = db.JuryDetail
                        .Where(x => x.ProposalId == proposalId &&
                            x.Phase == phase &&
                            x.EmailJury == email
                        ).FirstOrDefault();
                db.Remove(q);
                db.SaveChanges();
                return Ok("Data deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
