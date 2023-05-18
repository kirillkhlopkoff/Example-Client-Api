using TestApi_ClientMVC.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add configuration to the builder
builder.Configuration.AddJsonFile("appsettings.json", optional: true);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<PersonController>()
    .ConfigureHttpClient((services, client) =>
    {
        var configuration = services.GetRequiredService<IConfiguration>();
        var baseUrl = configuration.GetConnectionString("ApiBaseUrl");
        client.BaseAddress = new Uri(baseUrl);
    });

var app = builder.Build();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
