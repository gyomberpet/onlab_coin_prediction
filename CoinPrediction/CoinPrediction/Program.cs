using CoinPrediction.DAL;
using CoinPrediction.DAL.AutoMapper;
using CoinPrediction.DAL.EfDbContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//builder.Services.AddDbContext<CryptoMarketContext>(optinBuilder =>
//    optinBuilder.UseCosmos(builder.Configuration.GetConnectionString("CosmosDbConnection"),
//        builder.Configuration.GetValue<string>("DbName"))
//);
builder.Services.AddDbContext<CryptoMarketContext>(
    o => o.UseSqlServer(builder.Configuration.GetConnectionString("MsSQLConnection")));

builder.Services.AddAutoMapper(typeof(CryptoMarketProfile));
builder.Services.AddScoped<ICoinRepository, CoinRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
