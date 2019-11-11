using System;
using System.Collections.Generic;

namespace Backend.Model
{
    public partial class ScoringDetail
    {
        public Guid ScoringDetailId { get; set; }
        public Guid JuryDetailId { get; set; }
        public Guid RubricDetailId { get; set; }
        public int Weight { get; set; }
        public int Score { get; set; }
        public DateTime Date { get; set; }

        public virtual JuryDetail JuryDetail { get; set; }
        public virtual RubricDetail RubricDetail { get; set; }
    }
}
