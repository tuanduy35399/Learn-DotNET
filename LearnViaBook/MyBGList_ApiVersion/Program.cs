using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyBGList", Version = "v1.0" });
    opt.SwaggerDoc("v2", new OpenApiInfo { Title = "MyBGList", Version = "v2.0" });
});
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
    opt.AddPolicy(name: "AnyOrigin_GetOnly",
        cfg =>
        {
            cfg.AllowAnyOrigin();
            cfg.AllowAnyHeader();
            cfg.WithMethods("GET");
        }
        );
});
builder.Services.AddApiVersioning(opt =>
{
    opt.ApiVersionReader = new UrlSegmentApiVersionReader(); //version sẽ được đọc từ url
    //Ex: /api/v1/BoardGames
    // /api/v2/BoardGames
    opt.AssumeDefaultVersionWhenUnspecified = true; //Nếu client không truyền version thì dùng version mặc định
    opt.DefaultApiVersion = new ApiVersion(1, 0); //Set version mặc định v1.0

});
//set up giao diện swagger/ API document 
builder.Services.AddVersionedApiExplorer(opt => 
{
    opt.GroupNameFormat = "'v'VVV";
    /*
     * Ký hiệu	Ý nghĩa
        V	    Major
        VV	    Major.Minor
        VVV	    Major.Minor.Patch
       v1
       v1.0
       v1.0.0
     */
    opt.SubstituteApiVersionInUrl = true; //thay {apiVersion} trong url bằng version thật
    /*
    Ví dụ khi viết route
        [Route("api/v{version:apiVersion}/[controller]")] 
    Khi chạy nó sẽ thành
        /api/v1/BoardGames
        /api/v2/BoardGames
    Dùng {version:apiVersion} thay vì {apiVersion} để .NET biết đây là tham số đặc biệt ko phải string bth
    Cùng với việc apiVersion sẽ giúp match với attribue [ApiVersion("1.0")] của 1 controller 
    cũng như kiểm tra format của nó luôn , những cái ko hợp lệ như vabc thì sẽ bị bỏ, chỉ chấp nhận
   v1.0, v1, v1.9.0,... 
     */
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction()) //nếu khác dev hoặc staging thì không cho xài swagger
                                     //Bảo vệ kép 
{
    if (app.Configuration.GetValue<bool>("UseSwagger"))
    {
        app.UseSwagger();
        app.UseSwaggerUI(opt =>
        {
            opt.SwaggerEndpoint(
                $"/swagger/v1/swagger.json",
                $"MyBGList v1");
            opt.SwaggerEndpoint(
                $"/swagger/v2/swagger.json",
                $"MyBGList v2");
        });

    }

    if (app.Configuration.GetValue<bool>("UseDevelopertExceptionPage"))
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/error");
    }
    
    app.MapGet("/v{version:ApiVersion}/error",
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [EnableCors("AnyOrigin")]
    [ResponseCache(NoStore=true)]
    () => Results.Problem());

    app.MapGet("v{version:ApiVersion}/error/test",
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [EnableCors("AnyOrigin")]
    [ResponseCache(NoStore =true)]
    //Lưu cache phía client 

    //[ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]

    //Nếu muốn lưu cache trên reverse proxy (cache trung gian=> lưu những file không nhạy cảm)

    //[ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any)]
    () => { throw new Exception("test"); });
    //Minimal API có lợi thế trong việc áp dụng chính sách custom cho 1 endpoint cụ thể
    //ngược lại nếu dùng Controller thay vì minimal API thì chỉ có cách khai báo toàn cục áp dụng chính 
    //sách cho tất cả endpoint 
    //app.MapControllers().RequireCors("AnyOrigin");
    //[EnableCors] xử lý preflight tốt hơn -> ta sẽ ưu tiên dùng
    app.MapGet("/get-only", [EnableCors("AnyOrigin_GetOnly")] () => "mày chỉ có thể get thôi");
    app.UseHttpsRedirection();

    app.UseCors();
    //app.UseCors("AnyOrigin"); //nếu muốn gọi custom cfg
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
