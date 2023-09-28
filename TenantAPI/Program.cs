using Application;
using Application.Interfaces;
using Domain.DTOs;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("AppDb");
builder.Services.AddTransient<IReceivablesService, Handler>();
builder.Services.AddTransient<DataSeeder>();
//Add Repository Pattern
builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(connectionString));


builder.Services.AddDbContext<DataContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//if (args.Length == 1 && args[0].ToLower() == "seeddata")
SeedData(app);

//Seed Data
void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using var scope = scopedFactory.CreateScope();
    var service = scope.ServiceProvider.GetService<DataSeeder>();
    service.Seed();
}

app.MapGet("/", async (ICustomersRepository repo) =>
{
    return Results.Ok((await repo.GetCustomers()).ToArray());

}).WithName("GetAllCustomers").WithOpenApi();

app.MapGet("/openInvoices", async (ICustomersRepository repo, IReceivablesService handler) => Results.Ok(await handler.GetOpenInvoices(repo)));

app.MapGet("/closedInvoices", async (ICustomersRepository repo, IReceivablesService handler) => Results.Ok(await handler.GetClosedInvoices(repo)));

app.MapPost("/receivables/{id}", async (int id, List<ReceivablesDTO> receivables, ICustomersRepository repo, IReceivablesService handler) =>
{
    var result = await handler.HandlePost(id, receivables, repo);
    if (result == null)
        return Results.NotFound();

    return Results.Created($"/receivables/{id}", result);
}
)
    .WithOpenApi(generatedOperation =>
{
    var parameter = generatedOperation.Parameters[0];
    parameter.Description = "The ID associated with the customer";
    generatedOperation.Summary = "This endpoint takes as an input receivables associated with a prospective customer";
    return generatedOperation;
});

app.Run();
