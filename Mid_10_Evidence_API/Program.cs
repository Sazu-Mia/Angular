using Microsoft.EntityFrameworkCore;
using Mid_10_Evidence_API.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CandidateDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("db")));


builder.Services.AddCors(p => p.AddPolicy("EnableCors", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
builder.Services.AddControllers()
     .AddNewtonsoftJson(option => {
         option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
         option.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
     });
var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseCors("EnableCors");

app.MapControllers();

app.Run();
