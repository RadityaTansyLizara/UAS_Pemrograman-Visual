using Microsoft.EntityFrameworkCore;
using BabyShopWeb2.Data;
using BabyShopWeb2.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel to use port 5055
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenLocalhost(5055);
});

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add session support
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add custom services
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IPdfService, PdfService>();
builder.Services.AddScoped<IFinancialService, FinancialService>();

// Add MongoDB services
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<IMongoProductService, MongoProductService>();
builder.Services.AddScoped<IMongoSyncService, MongoSyncService>();

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
app.UseSession(); // Add session middleware

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Ensure database is created with latest schema
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    
    // Hanya create database jika belum ada (tidak delete yang sudah ada)
    // Untuk reset database, hapus manual file babyshop.db
    context.Database.EnsureCreated();
    
    // Seed admin user jika belum ada
    if (!context.AdminUsers.Any())
    {
        var adminUser = new BabyShopWeb2.Models.AdminUser
        {
            Username = "admin",
            PasswordHash = "jSMHbaS/QnVJRt5q6xKufA8JX4d2nxPWw5J3+5JQZH4=", // admin123
            FullName = "Administrator",
            CreatedAt = DateTime.Now
        };
        context.AdminUsers.Add(adminUser);
        context.SaveChanges();
        Console.WriteLine("✅ Admin user created: username=admin, password=admin123");
    }
    else
    {
        Console.WriteLine("✅ Admin user already exists");
    }
    
    Console.WriteLine("✅ Database ready");
}

app.Run();
