using System;
using System.Collections.Generic;

namespace Backend.Model
{
    public partial class IaHideVoting
    {
        public Guid Id { get; set; }
        public byte Hide { get; set; }
        public DateTime EditDate { get; set; }
        public string EditBy { get; set; }
    }
}
