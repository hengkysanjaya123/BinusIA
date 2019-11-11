using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Backend.Model;

namespace Backend.Repositories
{
    public class VotingRepository
    {
        BinusIAContext db = new BinusIAContext();

        public IEnumerable<Voting> GetVotings(){
            var q = db.Voting.Join(db.Proposal, v => v.ProposalId, p => p.ProposalId, (v, p) => new { v, p})
                    .Select(x=> new Voting{
                        VotingId = x.v.VotingId,
                        ProposalId = x.v.ProposalId,
                        Email = x.v.Email,
                        Date = x.v.Date,
                        Proposal = x.p
                    });
            // Select(x=> 
            //     db.Proposal.Where(y=> y.ProposalId == x.ProposalId).FirstOrDefault() == null ? "": 
            //     db.Proposal.Where(y=> y.ProposalId == x.ProposalId).FirstOrDefault().ProposalTitle
            // ).ToList();
            return q;
        }

        public Voting GetVoting(Guid id){
            var q = db.Voting.Join(db.Proposal, v => v.ProposalId, p => p.ProposalId, (v, p) => new { v, p})
                    .Where(x=> x.v.VotingId == id).FirstOrDefault().v;
            return q;
        }

        public Tuple<Boolean, String, Voting> AddVoting(Voting voting) {
            try{
                db.Add(voting);
                db.SaveChanges();

                return Tuple.Create(true, "Data Inserted Successfully", voting);
            }
            catch (Exception ex)
            {
                return Tuple.Create(false, ex.Message, voting);
            }
        }

        public Tuple<Boolean, String, Voting> UpdateVoting(Voting voting){
            try{
                var q = db.Voting.Where(x=> x.VotingId == voting.VotingId).FirstOrDefault();
                q.ProposalId = voting.ProposalId;
                q.Email = voting.Email;
                q.Date = voting.Date;

                db.SaveChanges();

                return Tuple.Create(true, "Data updated successfully", voting);
            }catch(Exception ex){
                return Tuple.Create(false,ex.Message, voting);
            }
        }

        public Tuple<Boolean, String> DeleteVoting(Guid VotingId){
            try{
                var q = db.Voting.Where(x=> x.VotingId == VotingId).FirstOrDefault();
                db.Remove(q);
                db.SaveChanges();
                
                return Tuple.Create(true, "Data Deleted Successfully");
            }
            catch(Exception ex){
                return Tuple.Create(false, ex.Message);
            }
        }
    }
}
