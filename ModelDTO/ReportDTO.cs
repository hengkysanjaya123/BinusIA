using System;
using System.Collections.Generic;
using Backend.Model;

namespace Backend.ModelDTO
{
    public class ReportDTO
    {
        public string TeamName { get; set; }
        public string ProposalTitle { get; set; }
        public string Category { get; set; }
        public int Phase { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Department { get; set; }
        public string BusinessUnit { get; set; }

    }
}
