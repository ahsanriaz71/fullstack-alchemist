using Microsoft.Extensions.Options;
using RSSApp.Extensions;
using RSSApp.Extensions.EmailServices;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Configure ApplicationSettings using options pattern
builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection(ApplicationSettings.SectionKey));
builder.Services.AddSingleton(s => s.GetRequiredService<IOptions<ApplicationSettings>>().Value);

// Configure session with options
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Configure EmailConfiguration using options pattern
builder.Services.Configure<EmailConfigurationOption>(builder.Configuration.GetSection("EmailConfiguration"));
builder.Services.AddScoped<IEmailkitService, EmailkitService>();

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
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

