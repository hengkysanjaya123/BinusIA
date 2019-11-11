using System;
using System.Collections.Generic;

namespace Backend.Model
{
    public partial class JuryDetail
    {
        public JuryDetail()
        {
            ScoringDetail = new HashSet<ScoringDetail>();
        }

        public Guid JuryDetailId { get; set; }
        public Guid ProposalId { get; set; }
        public int Phase { get; set; }
        public string EmailJury { get; set; }
        public int ApproveScore { get; set; }
        public string ApprovalCode { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime? AssignDate { get; set; }

        public virtual Proposal Proposal { get; set; }
        public virtual ICollection<ScoringDetail> ScoringDetail { get; set; }
    }
}
