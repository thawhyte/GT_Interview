using ShopsRUsData.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using ShopsRUsData;
using AutoMapper;
using ShopsRULogic.Handler;
using ShopsRUsData.EntityFrameworkCore;
using ShopsRULogic.Interface;
using ShopsRULogic.Managers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers();


//builder.Services.AddDbContext<ApplicationDbContext>(c =>
//                c.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=ShopRusDB; Trusted_Connection=True;"));

string connString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<ShopsDbContext>(options => options.UseSqlServer(connString));//, x => x.MigrationsAssembly("ShopsRUsData.Migrations")));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddScoped<ICustomer, CustomerManager>();
builder.Services.AddScoped<IInvoice,InvoiceManager > ();
builder.Services.AddScoped<IDiscounts, DiscountsManager> ();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//Automapper configuration
var automapper = new MapperConfiguration(item => item.AddProfile(new AutomapperHandler()));
IMapper mapper = automapper.CreateMapper();
AmountToWords amountToWords = new();


builder.Services.AddSingleton(mapper);
builder.Services.AddSingleton(amountToWords);



var app = builder.Build();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var serviceProvider = app.Services.GetRequiredService<IServiceProvider>();
using (var scope = serviceProvider.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ShopsDbContext>();
    await SeedHelper.InitializeData(context);
}

app.Run();
