var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseMiddleware<SportsStoreWebApp.Middleware.RequestLoggerMiddleware>();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.MapStaticAssets();
app.UseAuthorization();


app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

Console.WriteLine("Thuc hanh LinQ co ban");

List<SportsStore.Domain.Models.Product> sampleProduct = new List<SportsStore.Domain.Models.Product>

{
    new SportsStore.Domain.Models.Product { ProductID = 1, Name = @"Bóng đá World
Cup", Description = "Bóng đá chính hãng", Price = 50.00m, Category = "Bóng đá" },
new SportsStore.Domain.Models.Product { ProductID = 2, Name = "Áo đấu CLB A",
Description = "Áo đấu cho người hâm mộ", Price = 75.50m, Category = "Quần áo" },
new SportsStore.Domain.Models.Product { ProductID = 3, Name = @"Vợt Tennis
Pro", Description = "Vợt chuyên nghiệp", Price = 150.00m, Category = "Tennis" },
new SportsStore.Domain.Models.Product { ProductID = 4, Name = @"Giày chạy bộ
ABC", Description = "Giày thể thao nhẹ", Price = 99.99m, Category = "Giày" },
new SportsStore.Domain.Models.Product { ProductID = 5, Name = "Bóng rổ NBA",
Description = "Bóng rổ tiêu chuẩn", Price = 45.00m, Category = "Bóng rổ" }

};

Console.WriteLine("Loc san pham co gia tren 70");
var expensiveProduct = sampleProduct.Where(p => p.Price >= 70.00m);

foreach(var p in expensiveProduct)
{
    Console.WriteLine($"{p.Name} - {p.Price}");
}
app.Run();
