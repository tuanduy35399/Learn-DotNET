using LearnViaActionBook;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args); //tạo 1 thể hiện của webapplicationbuilder

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpLogging(log =>
log.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProperties
                        | HttpLoggingFields.RequestMethod
                        | HttpLoggingFields.RequestQuery
                        | HttpLoggingFields.ResponseStatusCode

// Nếu muốn ghi lại TẤT CẢ mọi thứ (Path, Headers, Body, v.v.)
// log.LoggingFields = HttpLoggingFields.All;
// Tùy chọn: Giới hạn độ dài log cho Body (mặc định là 32KB) để tránh quá tải
    //log.RequestBodyLogLimit = 4096;
// log.ResponseBodyLogLimit = 4096;
);
builder.Logging.AddFilter("Microsoft.AspNetCore.HttpLogging", LogLevel.Information);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpLogging();
}
//app.MapGet("/", ()=>"Hello World!" );
app.UseHttpsRedirection();

app.UseAuthorization();
//app.UseWelcomePage();
app.MapControllers();
app.MapGet("/", () => "Hello World");
app.MapGet("/person", () => new Person("Duy", "Tran") );
app.Run();
