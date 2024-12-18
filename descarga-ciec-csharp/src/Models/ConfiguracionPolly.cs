using System;
using System.Collections.Generic;
using System.Text;

namespace descarga_ciec_sdk.src.Models
{
    public class ConfiguracionPolly
    {
        /// <summary>
        /// TimeoutSeconds
        /// </summary>
        public int TimeoutSeconds { get; set; } = 60;

        /// <summary>
        /// RetryCount
        /// </summary>
        public int RetryCount { get; set; } = 0;

        /// <summary>
        /// SleepDurationSeconds
        /// </summary>
        public int SleepDurationSeconds { get; set; } = 30;

        /// <summary>
        /// HandledEventsAllowedBeforeBreaking
        /// </summary>
        public int HandledEventsAllowedBeforeBreaking { get; set; } = 10;

        /// <summary>
        /// 60 segundos = 1 mimuto.
        /// 1800 = 30 minutos
        /// </summary>
        public int DurationOfBreakSeconds { get; set; } = 1800;
    }
}
