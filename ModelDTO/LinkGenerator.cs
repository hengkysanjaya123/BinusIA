using System;
using System.Collections.Generic;

namespace Backend.ModelDTO
{
    public static class LinkGenerator
    {
        public static List<Link> GenerateLink(string href, Guid id, bool excludeID = false)
        {
            if (excludeID == false)
            {
                href = href + "/" + id.ToString();
            }

            return new List<Link>(){
                new Link() { Href = href, Rel = "self", Type = "GET" },
                new Link() { Href = href , Rel = "update", Type = "PUT" },
                new Link() { Href = href , Rel = "delete", Type = "DELETE" }
            };
        }
    }
}
