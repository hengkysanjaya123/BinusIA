using System;
using System.Collections.Generic;

namespace Backend.Model
{
    public partial class IaActivityLog
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
        public string Activity { get; set; }
        public DateTime Date { get; set; }
        public string Ip { get; set; }
    }
}
