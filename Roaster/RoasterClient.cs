using Newtonsoft.Json;
using Roaster.Enums;
using Roaster.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Roaster
{
    public class RoasterClient
    {
        private async Task<WebResult<T>> GetPostResultAsync<T>(string uri, Dictionary<string, string> headerValues)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var content = new FormUrlEncodedContent(headerValues);

                    var response = await client.PostAsync(uri, content);

                    var responseText = await response.Content.ReadAsStringAsync();

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
                return new WebResult<T>
                {
                    Status = ResultStatus.Failure,
                    Exception = ex
                };
            }
        }
    }
}
