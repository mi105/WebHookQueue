namespace WebHookQueue
{
    public class WebHookInfoRepository
    {
        private WebHookInfoContext context;

        public WebHookInfoRepository(WebHookInfoContext context)
        {
            this.context = context;
        }


        public async Task StoreQueueToDb()
        {
            var queue = ConcurrentQueueSingleton.Instance.WebHookInfoDtos;
               
            while (!queue.IsEmpty)
            {               
                var canConnect = await context.Database.CanConnectAsync();
                if (canConnect)
                {
                    var dequeued = queue.TryDequeue(out var webHookInfoDto);
                    if (dequeued && webHookInfoDto != null)
                    {
                        WebHookInfoModel model = new WebHookInfoModel(webHookInfoDto.Date, webHookInfoDto.Json);
                        await context.AddAsync(model);
                        await context.SaveChangesAsync();
                    }

                }               

            }
        }

        public async Task TrySaveToDb(WebHookInfoDto webHookInfoDto) 
        {
            WebHookInfoModel model = new WebHookInfoModel(webHookInfoDto.Date, webHookInfoDto.Json);
            await context.AddAsync(model);
            await context.SaveChangesAsync();

        }
    }
}
