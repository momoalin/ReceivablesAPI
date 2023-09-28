using TenantAPI.Infrastructure;
using TenantAPI.Models;
using TenantAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("AppDb");
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

}
).WithName("GetCustomersReceivables")
.WithOpenApi();


app.MapGet("/openInvoices", async (ICustomersRepository repo) =>
{
    var customers = (await repo.GetCustomers()).ToList();
    var openAmount = customers.Where(w => w.Recieveables is not null && w.Recieveables.Any())
    .Select(a => new { customer_id = a.Id, receivables = a.Recieveables.Where(r => r.ClosedDate is null).Sum(b => b.PaidValue) });
    return Results.Ok(openAmount);
}
);


app.MapGet("/closedInvoices", async (ICustomersRepository repo) =>
{
    var customers = (await repo.GetCustomers()).ToList();
    var openAmount = customers.Where(w => w.Recieveables is not null && w.Recieveables.Any())
    .Select(a => new { customer_id = a.Id, receivables = a.Recieveables.Where(r => r.ClosedDate is not null).Sum(b => b.PaidValue) });
    return Results.Ok(openAmount);
}
);

app.MapPost("/receivables/{id}", async (int id, List <TenantAPI.DTOs.ReceivablesDTO> receivables, ICustomersRepository repo) =>
{
    var entities = receivables.Select(r => r.ToReceiveable());
    var customer = await repo.GetCustomerById(id);
    if (customer is null)
        return Results.NotFound();
    customer.Recieveables?.AddRange(entities);
    await repo.Update(customer);
    return Results.Created($"/receivables/{id}", customer);
}
).WithOpenApi(generatedOperation =>
{
    var parameter = generatedOperation.Parameters[0];
    parameter.Description = "The ID associated with the customer"; 
    return generatedOperation;
}); ;

app.Run();


/*
 Validate fields, OpenAPI, CRUD, Business Logic/analytics, Tests, Done
    Debtor address validation (same address, dont overwrite. Different address?overwrite)
    Nullables validation
 
 */