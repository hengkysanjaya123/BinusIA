using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Backend.Model
{
    public partial class TeamHeader
    {
        public TeamHeader()
        {
            Proposal = new HashSet<Proposal>();
            TeamDetail = new HashSet<TeamDetail>();
        }

        public Guid TeamId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Photo { get; set; }

        [JsonIgnore]
        public virtual ICollection<Proposal> Proposal { get; set; }
        [JsonIgnore]
        public virtual ICollection<TeamDetail> TeamDetail { get; set; }
    }
}
