using Roaster.Enums;
using Roaster.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Roaster.Sample
{
    public class CustomizedRoasterClient : RoasterClient
    {
        protected override async Task<RoasterResult<T>> ProcessError<T>(HttpResponseMessage response, string responseText, Exception ex)
        {
            if (response?.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new RoasterResult<T>
                {
                    Exception = ex,
                    Message = "It's all good",
                    Status = ResultStatus.Failure
                };
            }

            return new RoasterResult<T>
            {
                Exception = ex,
                Message = "Custom error message",
                Status = ResultStatus.Failure
            };
        }
    }
}
