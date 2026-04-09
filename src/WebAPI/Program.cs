using LineManagement.API;
using Traceability.API;
using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddDefaults("Manufacturing-Execution-System");

// Modules
builder.AddLineManagement();
builder.AddTraceability();

var app = builder.Build();

app.UseDefaults();

app.Run();
