using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UpdevCommunity.Areas.Identity;
using UpdevCommunity.Data;
using UpdevCommunity.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<UpdevDbContext>(dbContextOptions => dbContextOptions
               .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
               .LogTo(Console.WriteLine, LogLevel.Information)
               .EnableSensitiveDataLogging()
               .EnableDetailedErrors());
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<UpdevUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<UpdevDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddAuthentication(o =>
{
    o.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
    .AddCookie(o => o.SlidingExpiration = true) 
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = "466029207334-4cpshqckildg39o13ocapgcanarrvavh.apps.googleusercontent.com";
        googleOptions.ClientSecret = "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";
    })
    .AddFacebook(facebookOptions =>
    {
        facebookOptions.AppId = "Authentication:Facebook:AppId";
        facebookOptions.AppSecret = "Authentication:Facebook:AppSecret";
    });
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<UpdevUser>>();
builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDoc();
builder.Services.AddAuthenticationJWTBearer("UpdevCommunityjeiwooeiwoei");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseFastEndpoints(config =>
{
    config.SerializerOptions = o => o.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    config.RoutingOptions = o => o.Prefix = "api";
    config.VersioningOptions = o =>
    {
        o.Prefix = "v";
        o.SuffixedVersion = false;
    };
});
app.MapControllers();
app.MapBlazorHub();
app.UseOpenApi();
app.UseSwaggerUi3(c => c.ConfigureDefaults());
app.MapFallbackToPage("/_Host");

app.Run();
