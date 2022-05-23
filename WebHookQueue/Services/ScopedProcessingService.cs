using System.Diagnostics;

namespace WebHookQueue.Services
{
    internal interface IScopedProcessingService
    {
        Task DoWork(CancellationToken stoppingToken);
    }

    internal class ScopedProcessingService : IScopedProcessingService
    {
        private const int MillisecondsDelay = 30000;
        private int executionCount = 0;
        private readonly WebHookInfoRepository _webHookInfoRepository;

        public ScopedProcessingService( WebHookInfoRepository webHookInfoRepository)
        {
            _webHookInfoRepository = webHookInfoRepository;
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                executionCount++;

                await Task.Delay(MillisecondsDelay);

                Debug.WriteLine(
                    $"Scoped Processing Service is working. Count: {executionCount}");
                await _webHookInfoRepository.StoreQueueToDb();
            }
        }
    }
}
