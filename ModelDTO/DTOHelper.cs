using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Backend.ModelDTO
{
    public static class DTOHelper
    {  
        // function to generate href path for hateos links
        public static string GetHref(HttpRequest Request)
        {
            return new UriBuilder
            {
                Scheme = Request.Scheme,
                Host = Request.Host.Host,
                Port = Request.Host.Port.Value,
                Path = Request.Path,
            }.Uri.ToString();
        }
    }
}
