using System;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace LunchTinderAzureTimer
{
    public class Function1
    {
        [FunctionName("Function1")]
        public void Run([TimerTrigger("0 */15 * * * *")]TimerInfo myTimer, ILogger log)
        {
			log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
			HttpClient c = new HttpClient();
			string ApiUrl = Environment.GetEnvironmentVariable("ApiUrl");
			var result = c.GetAsync(ApiUrl).Result.ToString();
			log.LogInformation($"{result}");
		}
    }
}
