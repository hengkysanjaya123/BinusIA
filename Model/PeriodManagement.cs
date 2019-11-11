using System;
using System.Collections.Generic;

namespace Backend.Model
{
    public partial class PeriodManagement
    {
        public Guid PeriodId { get; set; }
        public int Year { get; set; }
        public int Period { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime Date { get; set; }
        public bool Active { get; set; }
    }
}
