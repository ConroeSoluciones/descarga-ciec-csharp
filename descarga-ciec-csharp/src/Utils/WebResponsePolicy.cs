using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using descarga_ciec_sdk.src.Interfaces;
using descarga_ciec_sdk.src.Models;
using Polly;
using Polly.Wrap;

namespace descarga_ciec_sdk.src.Utils
{
    public class WebResponsePolicy : IWebResponsePolicy
    {
        /// <summary>
        /// retrySystem.Net
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public Policy GetRetryPolicy(ConfiguracionPolly option)
        {
            return Policy
                .Handle<WebException>(r =>
                {
                    bool b = false;
                    if (r.Response != null)
                        b = (
                            r.Status == WebExceptionStatus.ProtocolError
                            && ((HttpWebResponse)r.Response).StatusCode
                                >= HttpStatusCode.InternalServerError
                        );
                    else
                        b = true;
                    return b;
                })
                .WaitAndRetry(
                    option.RetryCount,
                    retryAttempt => TimeSpan.FromSeconds(option.SleepDurationSeconds),
                    (exception, timeSpan, retries, context) =>
                    {
                        System.Diagnostics.Debug.WriteLine(
                            $"Reitento No: {retries} | Mensaje: {exception.Message} | Tiempo: {timeSpan}"
                        );
                    }
                );
        }

        /// <summary>
        /// retrySystem.Net
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public AsyncPolicy GetRetryPolicyAsync(ConfiguracionPolly option)
        {
            var result = Policy
                .Handle<WebException>(r =>
                {
                    bool b = false;
                    if (r.Response != null)
                        b = (
                            r.Status == WebExceptionStatus.ProtocolError
                            && ((HttpWebResponse)r.Response).StatusCode
                                >= HttpStatusCode.InternalServerError
                        );
                    else
                        b = true;
                    return b;
                })
                .WaitAndRetryAsync(
                    option.RetryCount,
                    retryAttempt => TimeSpan.FromSeconds(option.SleepDurationSeconds),
                    (exception, timeSpan, retries, context) =>
                    {
                        System.Diagnostics.Debug.WriteLine(
                            $"Reitento No: {retries} | Mensaje: {exception.Message} | Tiempo: {timeSpan}"
                        );
                    }
                );

            return result;
        }

        /// <summary>
        /// circuitBreaker
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public Policy GetCircuitBreakerPolicy(ConfiguracionPolly option)
        {
            return Policy
                .Handle<WebException>()
                .CircuitBreaker(
                    option.HandledEventsAllowedBeforeBreaking,
                    TimeSpan.FromSeconds(option.DurationOfBreakSeconds)
                );
        }

        /// <summary>
        /// circuitBreaker
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public AsyncPolicy GetCircuitBreakerPolicyAsync(ConfiguracionPolly option)
        {
            return Policy
                .Handle<WebException>()
                .CircuitBreakerAsync(
                    option.HandledEventsAllowedBeforeBreaking,
                    TimeSpan.FromSeconds(option.DurationOfBreakSeconds)
                );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public PolicyWrap GetPolicies(ConfiguracionPolly option)
        {
            return Policy.Wrap(GetRetryPolicy(option), GetCircuitBreakerPolicy(option));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public AsyncPolicyWrap GetPoliciesAsync(ConfiguracionPolly option)
        {
            return Policy.WrapAsync(
                GetRetryPolicyAsync(option),
                GetCircuitBreakerPolicyAsync(option)
            );
        }
    }
}
