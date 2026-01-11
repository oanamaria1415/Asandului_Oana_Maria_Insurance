using Asandului_Oana_Maria_Insurance.Data;
using Asandului_Oana_Maria_Insurance.Services;
using Microsoft.EntityFrameworkCore;





var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<InsuranceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("InsuranceContext")));

builder.Services.AddHttpClient<IChargesPredictionService, ChargesPredictionService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:51850");
});


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
