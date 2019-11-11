using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Backend.Model
{
    public partial class Proposal
    {
        public Proposal()
        {
            CommentHeader = new HashSet<CommentHeader>();
            IaLog = new HashSet<IaLog>();
            JuryDetail = new HashSet<JuryDetail>();
            ProposalDetail = new HashSet<ProposalDetail>();
            Voting = new HashSet<Voting>();
        }

        public Guid ProposalId { get; set; }
        public int Phase { get; set; }
        public Guid StatusId { get; set; }
        public string ProposalTitle { get; set; }
        public string Category { get; set; }
        public Guid TeamId { get; set; }
        public DateTime SubmitDate { get; set; }
        public string Summary { get; set; }
        public string Keyword { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ApprovalDate { get; set; }
        public DateTime FinalizedDate { get; set; }
        public string FinalizedBy { get; set; }
        public int? Order { get; set; }
        public virtual StatusProposal Status { get; set; }
        public virtual TeamHeader Team { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<CommentHeader> CommentHeader { get; set; }

        [JsonIgnore]
        public virtual ICollection<IaLog> IaLog { get; set; }

        // [JsonIgnore]
        public virtual ICollection<JuryDetail> JuryDetail { get; set; }

        [JsonIgnore]
        public virtual ICollection<ProposalDetail> ProposalDetail { get; set; }

        [JsonIgnore]
        public virtual ICollection<Voting> Voting { get; set; }
    }
}
