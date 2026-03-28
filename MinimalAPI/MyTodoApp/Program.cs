using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpLogging(opts =>
    opts.LoggingFields = HttpLoggingFields.RequestProperties);
builder.Logging.AddFilter(
    "Microsoft.AspNetCore.HttpLogging", LogLevel.Information
);
var app = builder.Build();
// if (app.Environment.IsDevelopment())
// {
//     app.UseDeveloperExceptionPage();
// }
app.UseStaticFiles();
app.UseHttpLogging();
app.UseWelcomePage();
// app.MapGet("/", () =>
// {
//     throw new Exception("This is a test exception.");
// });

app.MapGet("/todo", () =>
{

    List<Todo> todos = new List<Todo>
    {
        new Todo {Id= 1, Description= "Hello" },
        new Todo {Id= 2, Description= "Hi"}
    };
    return todos;
});
app.Run();
