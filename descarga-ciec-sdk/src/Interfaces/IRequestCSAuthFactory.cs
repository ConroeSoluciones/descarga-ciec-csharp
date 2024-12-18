using System;
using System.Collections.Generic;
using System.Text;
using descarga_ciec_sdk.src.Impl.Https;

namespace descarga_ciec_sdk.src.Interfaces
{
    public interface IRequestCSAuthFactory
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        RequestCSAuth NewContratacionRequest(string token);
    }
}
