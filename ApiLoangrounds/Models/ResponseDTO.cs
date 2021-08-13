using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiLoangrounds.Models
{
    public class ResponseDTO
    {
        public ResponseDTO()
        {
            Id = 0;
            mensaje = "";
        }

        public int? Id { get; set; }
        public string mensaje { get; set; }
    }
}