using ASC.DataAccess;
using ASC.DataAccess.Interfaces;
using ASC_Web.Configuration;
using ASC_Web.Data;
using ASC_Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;



var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddCongfig(builder.Configuration).AddMyDependencyGroup(); //Add revise
builder.Services.AddScoped<INavigationCacheOperations, NavigationCacheOperations>();

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<IdentityUser, IdentityRole>((options) =>
{
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.AddScoped<DbContext, ApplicationDbContext>();
//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddOptions();
builder.Services.Configure<ApplicationSetting>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Add application services.
builder.Services.AddTransient<IEmailSender, AuthMessageSender>();
builder.Services.AddTransient<ISmsSender, AuthMessageSender>();
//Addition lab4
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//End lab4
builder.Services.AddSingleton<IIdentitySeed, IdentitySeed>();
builder.Services.AddScoped<IUniOfWork, UnitOfWork>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

app.UseSession();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var storage = scope.ServiceProvider.GetRequiredService<IIdentitySeed>();
    await storage.Seed(
        scope.ServiceProvider.GetService<UserManager<IdentityUser>>(),
        scope.ServiceProvider.GetService<RoleManager<IdentityRole>>(),
        scope.ServiceProvider.GetService<IOptions<ApplicationSetting>>());
}

using (var scope = app.Services.CreateScope())
{
    var navigationCacheOperations = scope.ServiceProvider.GetRequiredService<INavigationCacheOperations>();
    await navigationCacheOperations.CreateNavigationCacheAsync();
}

app.Run();
