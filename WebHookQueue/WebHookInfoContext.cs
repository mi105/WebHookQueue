using Microsoft.EntityFrameworkCore;

namespace WebHookQueue
{
    public class WebHookInfoContext : DbContext
    {
        public DbSet<WebHookInfoModel> WebHooks { get; set; }


        public WebHookInfoContext( DbContextOptions<WebHookInfoContext> dbContextOptions) : base(dbContextOptions)
        {
        }

       
    }
}
