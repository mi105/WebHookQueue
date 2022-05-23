using System.Collections.Concurrent;

namespace WebHookQueue
{
    public sealed class ConcurrentQueueSingleton
    {
        public ConcurrentQueue<WebHookInfoDto> WebHookInfoDtos { get; private set; }
        // private ctor so no-one can create an instance
        private ConcurrentQueueSingleton() {

            this.WebHookInfoDtos = new ConcurrentQueue<WebHookInfoDto>();
        }

        // static field to hold the ConcurrentQueueSingleton
        // initialization is thread-safe because it is handled by the static ctor
        private readonly static ConcurrentQueueSingleton instance = new ConcurrentQueueSingleton();

        // public static ConcurrentQueueSingleton instance
        public static ConcurrentQueueSingleton Instance
        {
            get { return instance; }
        }
    }

}
