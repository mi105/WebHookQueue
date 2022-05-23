namespace WebHookQueue
{
    public class WebHookInfoModel
    {
        public Guid Id { get; private set; }

        public DateTime Date { get; private set; }

        public string Json { get; private set; }

        public WebHookInfoModel(DateTime Date, string Json) 
        {
            this.Id = Guid.NewGuid();
            this.Date = Date;
            this.Json = Json;
            
        }

        public WebHookInfoModel( Guid Id, DateTime Date, string Json)
        {
            this.Id = Id;
            this.Date = Date;
            this.Json = Json;
        }

    }
}