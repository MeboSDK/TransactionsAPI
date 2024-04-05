using Infrastructure;
using Application;
using TransactionsAPI.Abstractions;
using TransactionsAPI.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.
    AddApplication().
    AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<ITransactionsService,TransactionsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
