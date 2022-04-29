using CloudinaryDotNet;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Streamy.Core.Services;
using Streamy.Extensions;
using Streamy.Infrastructure.Data;
using Streamy.Infrastructure.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddApplicationDbContexts(builder.Configuration);

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
        options.SignIn.RequireConfirmedAccount = true
)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthentication()
    .AddFacebook(options =>
    {
        options.AppId = builder.Configuration.GetValue<string>("Facebook:AppId");
        options.AppSecret = builder.Configuration.GetValue<string>("Facebook:AppSecret");
    })
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration.GetValue<string>("Google:ClientId");
        options.ClientSecret = builder.Configuration.GetValue<string>("Google:ClientSecret");
    });
;
builder.Services.AddControllersWithViews()
    //.AddMvcOptions(o =>
    //{
    //    o.ModelBinderProviders.Insert(0, new DateTimeModelBinder())
    //})
    ;

var cloudinaryCloudName = builder.Configuration.GetValue<string>("Cloudinary:CloudName");
var cloudinaryKey = builder.Configuration.GetValue<string>("Cloudinary:Key");
var cloudinarySecret = builder.Configuration.GetValue<string>("Cloudinary:Secret");


Account cloudinaryAccount = new Account(
        cloudinaryCloudName,
        cloudinaryKey,
        cloudinarySecret);

Cloudinary cloudinary = new Cloudinary(cloudinaryAccount);
cloudinary.Api.Secure = true;

builder.Services.AddSingleton(cloudinary);
   
builder.Services.AddApplicationServices();

var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
