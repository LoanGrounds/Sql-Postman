using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiLoangrounds.Models
{
    public class FiltroPrestamo
    {
        public double? montoMax { get; set;}
        public double? maxInteres { get; set; }
        public int? minDiasT { get; set; }
        public int? minDiasCutoas { get; set; }
        public int? minCantCuotas { get; set; }
    }
}