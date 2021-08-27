using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiLoangrounds.Models
{
    public class Prestamo
    {
        public int? Id { get; set; }

        //FOREIGN KEYS:
        public int? IdDetallePrestamo { get; set; }
        public int? IdUsuarioPrestamista { get; set; }
        public int? IdUsuarioPrestador { get; set; }

    }
}