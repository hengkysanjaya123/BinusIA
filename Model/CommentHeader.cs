using System;
using System.Collections.Generic;

namespace Backend.Model
{
    public partial class CommentHeader
    {
        public Guid CommentId { get; set; }
        public Guid ProposalId { get; set; }
        public string JuryEmail { get; set; }
        public int SectionId { get; set; }
        public string Comment { get; set; }

        public virtual Proposal Proposal { get; set; }
    }
}
