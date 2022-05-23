using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using WebHookQueue;
using WebHookQueue.Services;


var jsonPath = @"C:\\Users\\milos\\AppData\\Local";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<WebHookInfoContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<WebHookInfoRepository>();
builder.Services.AddHostedService<ConsumeScopedServiceHostedService>();
builder.Services.AddScoped<IScopedProcessingService, ScopedProcessingService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Lifetime.ApplicationStarted.Register(OnStarted);


app.Lifetime.ApplicationStopping.Register(OnStopping);


app.Run();


void OnStarted()
{
    Task.Run(async () =>
    {
        var load = false;
        var webhookdtos = new List<WebHookInfoDto>();
        if (File.Exists(jsonPath))
            load = true;

        if (load)
        {
            var text = await File.ReadAllTextAsync(jsonPath);
            try
            {
                webhookdtos = JsonConvert.DeserializeObject<List<WebHookInfoDto>>(text);
                File.Delete(jsonPath);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error reading from file {jsonPath}: {ex.Message}");
            }

        }
        if (webhookdtos != null && webhookdtos.Count > 0)
        {
            webhookdtos.ForEach(dto => ConcurrentQueueSingleton.Instance.WebHookInfoDtos.Enqueue(dto));
        }

    });
}


void OnStopping()
{
    Task.Run(async () =>
    {
        try
        {
            var data = ConcurrentQueueSingleton.Instance.WebHookInfoDtos.ToList();
            data.Reverse();
            string text = JsonConvert.SerializeObject(data);
            await File.WriteAllTextAsync(jsonPath, text);
        }
        catch (global::System.Exception ex)
        {
            Debug.WriteLine($"Critical error saving data state {ex.Message}");
        }

    });

}
