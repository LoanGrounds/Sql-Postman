using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiLoangrounds.Models
{
    public class PrestamoRecomendadoDTO
    {

        public int? Id { get; set; }

        public int? IdDetalle{ get; set; }
        public string UserName { get; set; }
        public int? Monto { get; set; }
        public string UrlFoto { get; set; } = "";

    }
}