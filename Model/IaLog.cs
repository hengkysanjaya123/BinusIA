using System;
using System.Collections.Generic;

namespace Backend.Model
{
    public partial class IaLog
    {
        public Guid Id { get; set; }
        public string EmailTry { get; set; }
        public string EmailLogin { get; set; }
        public Guid ProposalId { get; set; }
        public DateTime Date { get; set; }
        public string Ip { get; set; }

        public virtual Proposal Proposal { get; set; }
    }
}
