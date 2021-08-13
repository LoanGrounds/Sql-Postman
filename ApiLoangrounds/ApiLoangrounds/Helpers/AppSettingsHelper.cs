using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;


namespace ApiLoangrounds.Helpers
{
    public class AppSettingsHelper
    {
        /// <summary>
        ///     Obtiene de la clave "key" del archivo de configuracion (*.config), en caso de no existir retorna "defaultValue".
        ///     Ejemplo:
        ///     <appSettings>
        ///         <add key="LogFile" value="D:\GestionSimple\Logs\Log.txt"/>
        ///     </appSettings>
        /// 
        /// </summary>
        /// <param name="key">Clave</param>
        /// <param name="defaultValue">Valor a devolver en caso de no existir.</param>
        /// <returns></returns>
        public static string GetAppSetting(string key, string defaultValue)
        {
            string strReturnValue = defaultValue;
            string strValue;

            strValue = ConfigurationManager.AppSettings[key];
            if (strValue != null)
            {
                strReturnValue = strValue;
            }
            return strReturnValue;
        }
    }
}