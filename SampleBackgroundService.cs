using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DotNetCoreWindowsService
{
    internal class SampleBackgroundService : BackgroundService
    {
        private readonly ILogger<SampleBackgroundService> _logger;
        private readonly IConfiguration _configuration;

        public SampleBackgroundService(
            ILogger<SampleBackgroundService> logger
            , IConfiguration configuration
            , IApplicationLifetime applicationLifetime
            , LoggingConfiguration.StopAndFlushLogging stopAndFlushLogging)
        {
            _logger = logger;
            _configuration = configuration;
            applicationLifetime.ApplicationStopped.Register(() => stopAndFlushLogging());
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var message = "Verifying configuration works: testProperty = " + _configuration["testProperty"];
            for (var _ = 0; _ < 1000; _++)
            {
                _logger.LogInformation(message);
                await Task.Delay(500, stoppingToken);
            }
        }
    }
}