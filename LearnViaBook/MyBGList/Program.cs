var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
app.MapGet("/error", () => Results.Problem());
//app.MapGet("/error/test", () => { 
//    throw new Exception("test"); 
//});
app.MapGet("/error/test", () => { throw new Exception("test"); });
//app.UseWelcomePage();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
