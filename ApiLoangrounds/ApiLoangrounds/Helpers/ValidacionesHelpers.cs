using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace ApiLoangrounds.Helpers
{
    public static class ValidacionesHelpers
    {
        public static bool esStringValido(string s)
        {
            return s.Trim().Length > 3;
        }

        public static bool esMailValido(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    
        public static bool esIdValido(int id)
        {
            return id > 0;
        }
        public static bool esPositivo(int n)
        {
            return n > 0;
        }

        public static bool esPositivo(double n)
        {
            return n > 0;
        }

        public static bool esFechaValida(DateTime fecha)
        {
            return DateTime.TryParse("", out fecha);
        }
        public static bool esDniValido(string dni)
        {
            return dni.Trim().Length == 8;
        }

            
    }
}