using System;
using System.Collections.Generic;
using System.Text;
using descarga_ciec_sdk.src.Models;
using Polly;
using Polly.Wrap;

namespace descarga_ciec_sdk.src.Interfaces
{
    public interface IPolicy
    {
        /// <summary>
        /// GetPolicies
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        PolicyWrap GetPolicies(ConfiguracionPolly option);

        /// <summary>
        /// GetPolicies
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        AsyncPolicyWrap GetPoliciesAsync(ConfiguracionPolly option);

        /// <summary>
        /// GetRetryPolicy
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        Policy GetRetryPolicy(ConfiguracionPolly option);

        /// <summary>
        /// GetCircuitBreakerPolicy
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        Policy GetCircuitBreakerPolicy(ConfiguracionPolly option);
    }
}
