using server.Hubs;
using server.Models;
using server.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddSignalR();
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<MongoDBServices>();
var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseCors(options =>
{
    options.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader()
           .SetIsOriginAllowed((hosts) => true);
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Battleship}/{action=Home}/{id?}");

app.MapHub<PlayersHub>("/playersHub");
app.Run();
