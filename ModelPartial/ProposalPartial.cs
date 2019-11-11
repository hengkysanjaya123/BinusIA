using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Backend.Model
{
    public partial class Proposal
    {
        [NotMapped]
        public decimal Score { get; set; }        
    }
}
