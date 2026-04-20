using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using MyBGList.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
));
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(cfg =>
    {
        //cfg.AllowAnyOrigin();//cái này chỉ đơn giản là vô hiệu hóa chính sách đồng nguồn
        //(same origin policy- SOP)
        //nó mất đi lớp bảo vệ
        cfg.WithOrigins(builder.Configuration["AllowedOrigins"]); //giới hạn những domain nằm trong file appsettings.json
        cfg.AllowAnyHeader();
        cfg.AllowAnyMethod();
    });
    opt.AddPolicy(name: "AnyOrigin", //tạo 1 policy custom 
    cfg =>
    {
        cfg.AllowAnyOrigin();
        cfg.AllowAnyHeader();
        cfg.AllowAnyMethod();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction()) //nếu khác dev hoặc staging thì không cho xài swagger
    //Bảo vệ kép 
{
    if(app.Configuration.GetValue<bool>("UseSwagger"))
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
        
}

if (app.Configuration.GetValue<bool>("UseDevelopertExceptionPage"))
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
}
app.MapGet("/error",[EnableCors("AnyOrigin")] () => Results.Problem());

app.MapGet("/error/test", [EnableCors("AnyOrigin")] () => { throw new Exception("test"); });
//Minimal API có lợi thế trong việc áp dụng chính sách custom cho 1 endpoint cụ thể
//ngược lại nếu dùng Controller thay vì minimal API thì chỉ có cách khai báo toàn cục áp dụng chính 
//sách cho tất cả endpoint 
//app.MapControllers().RequireCors("AnyOrigin");
//[EnableCors] xử lý preflight tốt hơn -> ta sẽ ưu tiên dùng
app.UseHttpsRedirection();

app.UseCors();
//app.UseCors("AnyOrigin"); //nếu muốn gọi custom cfg
app.UseAuthorization();

app.MapControllers();

app.Run();
