using Newtonsoft.Json;
using Roaster.Enums;
using Roaster.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Roaster
{
    public class RoasterClient
    {
        private TimeSpan _timeout;

        #region Creators
        /// <summary>
        /// Creates a new instance of the RoasterClient with a specific timeout setting
        /// </summary>
        /// <param name="timeout">The HttpClient timeout setting</param>
        /// <returns></returns>
        public static RoasterClient Create(TimeSpan timeout)
        {
            return new RoasterClient
            {
                _timeout = timeout
            };
        }
        /// <summary>
        /// Creates a new instance of the RoasterClient with the default timeout setting
        /// </summary>
        /// <returns></returns>
        public static RoasterClient Create()
        {
            return Create(TimeSpan.FromSeconds(100));
        }

        #endregion

        #region GetPostResultAsync

        /// <summary>
        /// Returns an awaitable result from a POST call
        /// </summary>
        /// <typeparam name="T">Type of the result</typeparam>
        /// <param name="uri">The POST call URI</param>
        /// <returns></returns>
        public async Task<WebResult<T>> GetPostResultAsync<T>(string uri)
        {
            return await GetPostResultAsync<T>(uri, new Dictionary<string, string>());
        }

        /// <summary>
        /// Returns an awaitable result from a POST call
        /// </summary>
        /// <typeparam name="T">Type of the result</typeparam>
        /// <param name="uri">The POST call URI</param>
        /// <param name="headerValues">The values to be passed in the header</param>
        /// <returns></returns>
        public async Task<WebResult<T>> GetPostResultAsync<T>(string uri, Dictionary<string, string> headerValues)
        {
            HttpResponseMessage response = null;
            string responseText = null;
            try
            {
                using (var client = new HttpClient() { Timeout = _timeout })
                {
                    var content = new FormUrlEncodedContent(headerValues);

                    response = await client.PostAsync(uri, content);

                    responseText = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<T>(responseText);

                    return new WebResult<T>
                    {
                        Status = ResultStatus.Success,
                        Result = result
                    };
                }
            }
            catch (Exception ex)
            {
                return await ProcessError<T>(response, responseText, ex);
            }
        }

        #endregion

        #region ProcessErrorAsync

        /// <summary>
        /// Processes the response data in case of an error and produces a result with status, message and exception based on that. Will return a general error status if not overriden.
        /// </summary>
        /// <typeparam name="T">Type of the result</typeparam>
        /// <param name="response">Http response</param>
        /// <param name="responseText">Parsed response text</param>
        /// <param name="ex">Exception thrown</param>
        /// <returns></returns>
        protected virtual async Task<WebResult<T>> ProcessError<T>(HttpResponseMessage response, string responseText, Exception ex)
        {
            return new WebResult<T>
            {
                Status = ResultStatus.Failure,
                Message = "Error",
                Exception = ex
            };
        }

        #endregion
    }
}
