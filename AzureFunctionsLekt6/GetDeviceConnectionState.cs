using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.Devices;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionsLekt6
{
    public static class GetDeviceConnectionState
    {
        private static readonly RegistryManager registryManager =
            RegistryManager.CreateFromConnectionString(Environment.GetEnvironmentVariable("IotHub"));

        [FunctionName("GetDeviceConnectionState")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "devices/connect")] HttpRequest req,
            ILogger log)
        {

            string deviceId = req.Query["deviceId"];
            var device = await registryManager.GetDeviceAsync(deviceId);
            if (device != null)
                return new OkObjectResult(device.ConnectionState.ToString());

            return new BadRequestResult();
        }
    }
}
