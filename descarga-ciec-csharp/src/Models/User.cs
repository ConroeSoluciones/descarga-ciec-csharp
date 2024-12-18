using System;
using System.Collections.Generic;
using System.Text;

namespace descarga_ciec_sdk.src.Models
{
    public class User
    {
        /// <summary>
        /// RFC contratción
        /// </summary>
        public string RFC { get; set; }

        /// <summary>
        /// Password contratación
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Token autenticación
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Razón Social contración
        /// </summary>
        public string RazonSocial { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="rfc"></param>
        /// <param name="password"></param>
        public User(string rfc, string password)
        {
            RFC = setRFC(rfc);
            Password = setPassword(password);
        }

        public User() { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="rfc"></param>
        /// <returns></returns>
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
