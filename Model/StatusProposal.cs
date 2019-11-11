using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Backend.Model
{
    public partial class StatusProposal
    {
        public StatusProposal()
        {
            Proposal = new HashSet<Proposal>();
        }

        public Guid StatusId { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Proposal> Proposal { get; set; }
    }
}
