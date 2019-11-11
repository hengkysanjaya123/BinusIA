using System;
using System.Collections.Generic;

namespace Backend.Model
{
    public partial class RubricDetail
    {
        public RubricDetail()
        {
            ScoringDetail = new HashSet<ScoringDetail>();
        }

        public Guid RubricDetailId { get; set; }
        public Guid RubricHeaderId { get; set; }
        public Guid RubricDescriptionId { get; set; }
        public int Weight { get; set; }

        public virtual RubricDescription RubricDescription { get; set; }
        public virtual RubricHeader RubricHeader { get; set; }
        public virtual ICollection<ScoringDetail> ScoringDetail { get; set; }
    }
}
