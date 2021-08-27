using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiLoangrounds.Models
{
    public class Usuario
    {
        public int? Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Mail { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Dni { get; set; }
        public string CBU { get; set; }
        public string CBUAlias { get; set; }
        public string CUIT { get; set; }
        public int? Puntos { get; set; }
        public string Descripcion { get; set; }
        public string Ocupacion { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? CantidadPrestamosExitosos { get; set; }
        public string URLFoto { get; set; }
        public string ApiKey { get; set; }

        //FOREIGN KEYS:
        public int? IdGenero { get; set; }
        public int? IdLocalidad { get; set; }

        public string NombreGenero { get; set; }
        public string NombreLocalidad { get; set; }



    }
}