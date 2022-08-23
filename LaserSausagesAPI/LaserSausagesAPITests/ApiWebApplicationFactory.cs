using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserSausagesAPITests
{
    internal class ApiWebApplicationFactory : WebApplicationFactory<Program>
    {
        public IConfiguration? Configuration { get; private set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            Environment.SetEnvironmentVariable("AZURE_STORAGETABLE_CONNECTIONSTRING", "DefaultEndpointsProtocol=https;AccountName=lasersausage;AccountKey=/Kq0wUdk7SdN8oKqy7GLjHvAcqdn6Fu1qLhw7XD/m/CN9imhrv6AmJc3lc2Zi9yVautpd0LRreJvBbTDHyXgIQ==;BlobEndpoint=https://lasersausage.blob.core.windows.net/;TableEndpoint=https://lasersausage.table.core.windows.net/;QueueEndpoint=https://lasersausage.queue.core.windows.net/;FileEndpoint=https://lasersausage.file.core.windows.net/");

            builder.ConfigureAppConfiguration(config => { });

            builder.ConfigureTestServices(services => { });
        }
    }
}
