using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WebHookQueue.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebHookInfoController : ControllerBase
    {

        private WebHookInfoRepository repository;

        public WebHookInfoController(WebHookInfoRepository repo)
        {
            this.repository = repo;
        }



        [HttpPost(Name = "PostWebHookInfo")]
        public async Task Post(WebHookInfoDto webHookInfoDto)
        {
            await Task.Run(()=>ConcurrentQueueSingleton.Instance.WebHookInfoDtos.Enqueue(webHookInfoDto));
        }
    }
}