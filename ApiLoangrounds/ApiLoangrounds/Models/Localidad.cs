using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiLoangrounds.Models
{
    public class Localidad
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }

        //FOREING KEYS
        public int? IdDepartamento { get; set; }
    }
}