using Microsoft.AspNetCore.HttpOverrides;
using WEB_153502_KIRZNER.Helpers;
using WEB_153502_KIRZNER.Services.CategoryService;
using WEB_153502_KIRZNER.Services.ProductService;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// Add services to the container.
builder.Services.AddControllersWithViews();

// builder.Services.AddScoped<ICategoryService, MemoryCategoryService>();
// builder.Services.AddScoped<IProductService, MemoryProductService>();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultScheme = "cookie";
    opt.DefaultChallengeScheme = "oidc";
})
    .AddCookie("cookie")
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority =
        builder.Configuration["InteractiveServiceSettings:AuthorityUrl"];
        options.ClientId =
        builder.Configuration["InteractiveServiceSettings:ClientId"];
        options.ClientSecret =
        builder.Configuration["InteractiveServiceSettings:ClientSecret"];
        // Получить Claims пользователя
        options.GetClaimsFromUserInfoEndpoint = true;
        options.ResponseType = "code";
        options.ResponseMode = "query";
        options.SaveTokens = true;
    });

var configuration = builder.Configuration;

var uriData = new UriData
{
    ApiUri = configuration.GetSection("UriData").GetValue<string>("ApiUri")!,
    ISUri = configuration.GetSection("UriData").GetValue<string>("ISUri")!
};

builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient<IProductService, ApiProductService>(opt =>opt.BaseAddress = new Uri(uriData.ApiUri));
builder.Services.AddHttpClient<ICategoryService, ApiCategoryService>(opt =>opt.BaseAddress = new Uri(uriData.ApiUri));


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
app.UseAuthentication();

app.MapRazorPages().RequireAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();

