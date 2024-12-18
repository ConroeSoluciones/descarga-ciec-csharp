using System;
using System.Collections.Generic;
using System.Text;

namespace descarga_ciec_sdk.src.Enums
{
    public enum TipoEstatusConsulta
    {
        EN_ESPERA, // 0
        PROCESANDO, // 1
        DESCARGANDO, // 2
        COMPLETADO, // 3
        FALLO, // 4
        DESCARGANDO_ZIP, // 5
        FALLO_SERVIDOR, //6,
        COMPLETADO_CON_FALTANTES, // 7
        FALLO_REPETIR, //8
        TERMINADA, //9
        REPETIR, //10
    }
}
