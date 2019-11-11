using System;
using System.Collections.Generic;

namespace Backend.Model
{
    public partial class TemplateProposal
    {
        public TemplateProposal()
        {
            TemplateProposalDetail = new HashSet<TemplateProposalDetail>();
        }

        public Guid ChapterId { get; set; }
        public int Phase { get; set; }
        public string Chapter { get; set; }
        public int Year { get; set; }

        public virtual ICollection<TemplateProposalDetail> TemplateProposalDetail { get; set; }
    }
}
