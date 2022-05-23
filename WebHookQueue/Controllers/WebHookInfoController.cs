using Microsoft.AspNetCore.Mvc;

namespace WebHookQueue.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebHookInfoController : ControllerBase
    {

        private WebHookInfoContext context;

        public WebHookInfoController(WebHookInfoContext context)
        {
            this.context = context;
        }



        [HttpPost(Name = "PostWebHookInfo")]
        public async Task<WebHookInfoDto> Post(WebHookInfoDto webHookInfoDto)
        {
            WebHookInfoModel model = new WebHookInfoModel(webHookInfoDto.Date, webHookInfoDto.Json);
            await context.AddAsync(model);
            await context.SaveChangesAsync();
            return webHookInfoDto;
        }
    }
}