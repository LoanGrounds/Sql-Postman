using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiLoangrounds
{
    /// <summary>
    /// Validaciones previas antes de hacer cada request
    /// </summary>
    public class HandlerApi : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "texto/normal";
            context.Response.Write("Hola a todos");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}