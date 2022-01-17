using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestApp.Data;
using TestApp.Models;

var builder = WebApplication.CreateBuilder(args);

// DIの設定をここでしている
// ここでサービス登録をしていない場合、シードデータが投入されない動作をする
builder.Services.AddDbContext<TestAppContext>(options =>
    options.UseSqlServer(
        // appsettings.json から引数名の接続文字列を取得(appsettings.json の10行目の値)
        builder.Configuration.GetConnectionString("TestAppContext")
    )
);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// SeedDataの設定
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        MoviesSeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    // ここでルーティングを設定
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
