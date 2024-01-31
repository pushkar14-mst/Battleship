using server.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddSignalR();
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
