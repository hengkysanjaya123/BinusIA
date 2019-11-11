using System;
using System.Collections.Generic;

namespace Backend.Model
{
    public partial class Voting
    {
        public Guid VotingId { get; set; }
        public Guid ProposalId { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public virtual Proposal Proposal { get; set; }
    }
}
