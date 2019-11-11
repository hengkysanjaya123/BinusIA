using System;
using System.Collections.Generic;

namespace Backend.Model
{
    public partial class RubricDescription
    {
        public RubricDescription()
        {
            RubricDetail = new HashSet<RubricDetail>();
        }

        public Guid RubricDescriptionId { get; set; }
        public string Criteria { get; set; }
        public string Bad { get; set; }
        public string Good { get; set; }
        public string Distinction { get; set; }
        public string Excellence { get; set; }

        public virtual ICollection<RubricDetail> RubricDetail { get; set; }
    }
}
