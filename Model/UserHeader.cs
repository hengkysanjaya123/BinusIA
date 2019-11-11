using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Backend.Model
{
    public partial class UserHeader
    {
        public UserHeader()
        {
            InverseLead = new HashSet<UserHeader>();
        }

        public string BinusianId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public string BusinessUnit { get; set; }
        public string Extension { get; set; }
        public string Handphone { get; set; }
        public string JobBand { get; set; }
        public string LeadId { get; set; }
        public int Role { get; set; }

        public virtual UserHeader Lead { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserHeader> InverseLead { get; set; }
    }
}
