using Roaster.Enums;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Roaster.Sample
{
    class Program
    {
        private const string _testApiUri = "https://jsonplaceholder.typicode.com/photos";

        static async Task Main(string[] args)
        {
            WriteLog("Running the default client with a valid URI");
            await UseDefaultClient(_testApiUri);

            Console.WriteLine();

            WriteLog("Running the default client with an invalid URI");
            await UseDefaultClient("invalid uri");

            Console.WriteLine();

            WriteLog("Running the customized client with a valid URI");
            await UseCustomizedClient(_testApiUri);

            Console.WriteLine();

            WriteLog("Running the customized client with an invalid URI");
            await UseCustomizedClient("invalid uri");

            Console.WriteLine();

            WriteLog("Done");
            Console.ReadKey();
        }

        private static async Task UseDefaultClient(string uri)
        {
            var defaultClient = new RoasterClient();

            var postData = new Dictionary<string, string>
            {
                { "albumId", "1" },
                { "title", "The ultimate placeholder photo" },
                { "url", "https://via.placeholder.com/600/92c952"},
                { "thumbnailUrl", "https://via.placeholder.com/150/92c952"}
            };

            var userDetailsRequest = await defaultClient.GetPostResultAsync<Photo>(uri, postData);

            if(userDetailsRequest.Status == ResultStatus.Success)
            {
                Console.WriteLine($"Successfully created a photo with Id = {userDetailsRequest.Result.Id}");
            }else if(userDetailsRequest.Status == ResultStatus.Unauthorized)
            {
                Console.WriteLine("You are unauthorized, try logging in first.");
            }
            else
            {
                Console.WriteLine(userDetailsRequest.Message);
            }
        }

        private static async Task UseCustomizedClient(string uri)
        {
            var defaultClient = new CustomizedRoasterClient();

            var postData = new Dictionary<string, string>
            {
                { "albumId", "1" },
                { "title", "The ultimate placeholder photo" },
                { "url", "https://via.placeholder.com/600/92c952"},
                { "thumbnailUrl", "https://via.placeholder.com/150/92c952"}
            };

            var userDetailsRequest = await defaultClient.GetPostResultAsync<Photo>(uri, postData);

            if (userDetailsRequest.Status == ResultStatus.Success)
            {
                Console.WriteLine($"Successfully created a photo with Id = {userDetailsRequest.Result.Id}");
            }
            else if (userDetailsRequest.Status == ResultStatus.Unauthorized)
            {
                Console.WriteLine("You are unauthorized, try logging in first.");
            }
            else
            {
                Console.WriteLine(userDetailsRequest.Message);
            }
        }

        private static void WriteLog(string logText)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(logText);
            Console.ForegroundColor = color;
        }
    }
}
