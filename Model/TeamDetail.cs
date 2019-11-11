using System;
using System.Collections.Generic;

namespace Backend.Model
{
    public partial class TeamDetail
    {
        public Guid TeamDetailId { get; set; }
        public Guid TeamId { get; set; }
        public string EmailMember { get; set; }
        public string Role { get; set; }
        public string SizeShirt { get; set; }

        public virtual TeamHeader Team { get; set; }
    }
}
