using System;
using System.Collections.Generic;
using System.Text;

namespace descarga_ciec_sdk.src.Utils
{
    public class DatetimeUtil
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static string convertDateTimeToStringFormat_yyyy_MM_ddT23_59_59(DateTime fecha)
        {
            string fechaFormat;

            fechaFormat = Convert.ToDateTime(fecha).ToString("yyyy-MM-ddT23:59:59");

            return fechaFormat;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static string convertDateTimeToStringFormat_yyyy_MM_ddT00_00_00(DateTime fecha)
        {
            string fechaFormat;

            fechaFormat = Convert.ToDateTime(fecha).ToString("yyyy-MM-ddT00:00:00");

            return fechaFormat;
        }
    }
}
