using System;
using System.Collections.Generic;

namespace Backend.Model
{
    public partial class ProposalDetail
    {
        public Guid ProposalDetailId { get; set; }
        public Guid ProposalId { get; set; }
        public Guid SubchapterId { get; set; }
        public string Subchapter { get; set; }
        public string Content { get; set; }
        public int? Editing { get; set; }
        public DateTime LastEdited { get; set; }
        public string EditedBy { get; set; }

        public virtual Proposal Proposal { get; set; }
        public virtual TemplateProposalDetail SubchapterNavigation { get; set; }
    }
}
