using Azure.Identity;
using Fluxor;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using RACE2.DataAccess.Repository;
using RACE2.FrontEndWebapp.Components;
using RACE2.FrontEndWebapp.Data;
using RACE2.FrontEndWebapp.Identity;
using RACE2.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddAzureAppConfiguration(options =>
{
    //var connectionString = builder.Configuration["AZURE_APPCONFIGURATION_CONNECTIONSTRING"];
    var azureAppConfigUrl = builder.Configuration["AzureAppConfigURL"];
    var credential = new DefaultAzureCredential();

    //options.Connect(connectionString)      
    options.Connect(new Uri(azureAppConfigUrl), credential)
    .ConfigureKeyVault(options =>
    {
        options.SetCredential(credential);
    })
    .ConfigureRefresh(refreshOptions =>
            refreshOptions.Register("refreshAll", refreshAll: true))
    .Select(KeyFilter.Any, LabelFilter.Null)
    // Override with any configuration values specific to current hosting env
    .Select(KeyFilter.Any, Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
    .UseFeatureFlags();
});
var blazorClientURL = builder.Configuration["RACE2FrontEndURL"];
var RACE2WebApiURL = builder.Configuration["RACE2WebApiURL"];
var RACE2IDPURL = builder.Configuration["RACE2SecurityProviderURL"];
var ClientSecret = builder.Configuration["ClientSecret"];

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<UserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});
builder.Services.AddHttpContextAccessor();

bool requireHttpsMetadata = builder.Environment.IsProduction();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
//.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.Cookie.MaxAge = options.ExpireTimeSpan;
    options.SlidingExpiration = true;
    options.EventsType = typeof(CustomCookieAuthenticationEvents);//forcibly expire after a day
})
.AddOpenIdConnect(
    OpenIdConnectDefaults.AuthenticationScheme,
    options =>
    {
        //options.Events.OnTicketReceived = async (Context) =>
        //{
        //    Context.Properties.ExpiresUtc = DateTime.UtcNow.AddMinutes(30);
        //};
        options.Events.OnRedirectToIdentityProvider = context =>
        {
            context.ProtocolMessage.Prompt = "login";
            return Task.CompletedTask;
        };
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.SignOutScheme = OpenIdConnectDefaults.AuthenticationScheme;
        options.Authority = RACE2IDPURL;
        options.ClientId = "blazorServer";
        options.ClientSecret = ClientSecret;
        // When set to code, the middleware will use PKCE protection
        options.ResponseType = "code id_token";
        // Save the tokens we receive from the IDP
        options.SaveTokens = true; // false;
        // It's recommended to always get claims from the UserInfoEndpoint during the flow.
        options.GetClaimsFromUserInfoEndpoint = true;
        options.Scope.Add("race2WebApi");
        options.RequireHttpsMetadata = requireHttpsMetadata;
    });

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

builder.Services.AddFluxor(o =>
{
    o.ScanAssemblies(typeof(Program).Assembly);
    o.UseReduxDevTools(rdt => { rdt.Name = "RACE2 application"; });
});

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReservoirService, ReservoirService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IReservoirRepository, ReservoirRepository>();
builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();
builder.Services.AddScoped<IOpenXMLUtilitiesService, OpenXMLUtilitiesService>();
builder.Services.AddScoped<CustomCookieAuthenticationEvents>();

//builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
//    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender, NoOpEmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
