using System;
using System.Collections.Generic;

namespace Backend.ModelDTO
{
    public interface IDTO
    {
        ICollection<Link> Links { get; set; }
    }
}
