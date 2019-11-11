using System;
using System.Collections.Generic;
using Backend.Model;

namespace Backend.ModelDTO
{
    public class ProposalDTO : IDTO
    {
        public Proposal Value { get; set; }
        public ICollection<Link> Links { get; set; }
    }
}
