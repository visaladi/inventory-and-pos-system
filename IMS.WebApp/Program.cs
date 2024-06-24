using IMS.Plugins.InMemory;
using IMS.UseCases.PluginInterfaces;
using IMS.WebApp.Components;
using IMS.UseCases.Products.interfaces;
using IMS.UseCases.Products;
using IMS.UseCases.Activities.Interfaces;
using IMS.UseCases.Activities;
using IMS.UseCases.Reports.interfaces;
using IMS.UseCases.Reports;
using Microsoft.EntityFrameworkCore;
using IMS.Plugins.EfCoreSqlServer;
using IMS.Plugins.EFCoreSqlServer;
using Microsoft.AspNetCore.Identity;
using IMS.WebApp.Data;
using IMS.UseCases;


var builder = WebApplication.CreateBuilder(args);


// Configuring Authorizations
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireClaim("Department", "Administration"));
    options.AddPolicy("Sales", policy => policy.RequireClaim("Department", "Sales"));
});

builder.Services.AddDbContext<AccountDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("InventoryManagement"));
});
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
}).AddRoles<IdentityRole>()
.AddEntityFrameworkStores<AccountDbContext>();              // Here, this order is critical

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
//builder.Services.AddCascadingAuthenticationState();

builder.Services.AddDbContextFactory<IMSContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("InventoryManagement"));
});





// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddRazorPages();

builder.Services.AddTransient<IProductRepository, ProductEFCoreRepository>();
builder.Services.AddTransient<IProductTransactionRepository, ProductTransactionEFCoreRepository>();

// Product UseCases
builder.Services.AddTransient<IViewProductsByNameUseCase, ViewProductsByNameUseCase>();
builder.Services.AddTransient<IAddProductUseCase, AddProductUseCase>();
builder.Services.AddTransient<IViewProductByIdUseCase, ViewProductByIdUseCase>();
builder.Services.AddTransient<IEditProductUseCase, EditProductUseCase>();

// Activity UseCases
builder.Services.AddTransient<ISellProductUseCase, SellProductUseCase>();
builder.Services.AddTransient<ISearchProductTransactionUseCase, SearchProductTransactionUseCase>();

// **Register IShoppingCartService** (New line)
//builder.Services.AddTransient<IShoppingCartService, ShoppingCartService>();

// S H O P P I N G    C A R T    D E P E N D E N C Y    I N J E C T I O N //
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
////////////////////////////////////////////////////////////////////////////


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

app.MapRazorPages();    // Crucial for identity web page rendering

//app.UseAuthentication();
//app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


app.Run();
