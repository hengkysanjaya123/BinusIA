using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Backend.Model
{
    public partial class UserHeader
    {
        [NotMapped]
        public string RoleName { get; set; }        
    }
}
