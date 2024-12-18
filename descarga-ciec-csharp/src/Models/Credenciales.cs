using System;
using System.Collections.Generic;
using System.Text;

namespace descarga_ciec_sdk.src.Models
{
    public class Credenciales
    {
        /// <summary>
        /// RFC empresa
        /// </summary>
        public string RFC { get; set; }

        /// <summary>
        /// Password CIEC SAT
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Razón Social empresa
        /// </summary>
        public string RazonSocial { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="rfc"></param>
        /// <param name="password"></param>
        public Credenciales(string rfc, string password)
        {
            RFC = setRFC(rfc);
            Password = setPassword(password);
        }

        public Credenciales() { }

        public string setRFC(string rfc)
        {
            return RFC = rfc;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string setPassword(string password)
        {
            return Password = password;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string getRFC()
        {
            return RFC;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string getPassword()
        {
            return Password;
        }
    }
}
