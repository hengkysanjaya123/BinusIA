using System;
using System.Collections.Generic;

namespace Backend.Model
{
    public partial class TemplateProposalDetail
    {
        public TemplateProposalDetail()
        {
            ProposalDetail = new HashSet<ProposalDetail>();
        }

        public Guid SubchapterId { get; set; }
        public Guid ChapterId { get; set; }
        public string Subchapter { get; set; }

        public virtual TemplateProposal Chapter { get; set; }
        public virtual ICollection<ProposalDetail> ProposalDetail { get; set; }
    }
}
