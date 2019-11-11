    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Backend.Model;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class ProposalRepository
    {
        BinusIAContext db = new BinusIAContext();

        public List<Proposal> GetProposals()
        {
            
            // var q = db.Proposal
            //         .Include(x => x.Status)
            //         .Include(x => x.Team)
            //             .ThenInclude(x => x.TeamDetail)
            //         .Include(x => x.CommentHeader)
            //         .Include(x => x.IaLog)
            //         .Include(x=> x.JuryDetail)
            //         .Include(x=> x.ProposalDetail)
            //         .ToList();


            // var q = db.Proposal
            // .Join(db.TeamHeader, p => p.TeamId, t => t.TeamId, (p, t) => new { p, t })
            // .Join(db.StatusProposal,
            //       p => p.p.StatusId,
            //       sp => sp.StatusId,
            //       (p, sp) => new { p = p.p, t = p.t, sp = sp })
            // .ToList().Select(x => x.p).ToList();

            var q = db.Proposal
                    .ToList();

            return q;
        }

        public List<Proposal> GetProposals(int year)
        {
            var q = db.Proposal.Where(x => x.SubmitDate.Year == year).ToList();
            return q;
        }

        public Proposal GetProposal(Guid id)
        {
            var q = db.Proposal.Where(x => x.ProposalId == id).FirstOrDefault();
            return q;
        }
    }
}
