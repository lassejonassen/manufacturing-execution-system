using LineManagement.API;
using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddDefaults("Manufacturing-Execution-System");

// Modules
builder.AddLineManagement();

var app = builder.Build();

app.UseDefaults();

app.Run();
