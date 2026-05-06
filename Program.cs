using Microsoft.EntityFrameworkCore;
using RestaurantApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddSession();

var app = builder.Build();



using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!context.Items.Any())
    {
        context.Items.AddRange(
            new Item { Name = "Sada Sandwich", Price = 45 },
            new Item { Name = "Diet Grilled", Price = 90 },
            new Item { Name = "Veg Burger", Price = 150 }
        );

        context.SaveChanges();
    }
}


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();