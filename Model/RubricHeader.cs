using System;
using System.Collections.Generic;

namespace Backend.Model
{
    public partial class RubricHeader
    {
        public RubricHeader()
        {
            RubricDetail = new HashSet<RubricDetail>();
        }

        public Guid RubricHeaderId { get; set; }
        public int Phase { get; set; }
        public string Description { get; set; }
        public string Year { get; set; }

        public virtual ICollection<RubricDetail> RubricDetail { get; set; }
    }
}
