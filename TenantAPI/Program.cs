using TenantAPI;
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

if (args.Length == 1 && args[0].ToLower() == "seeddata")
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
    return (await repo.GetCustomers()).ToArray();
    
}
);

app.Run();


/*
 Validate fields, OpenAPI, CRUD, Business Logic/analytics, Tests, Done
    Debtor address validation (same address, dont overwrite. Different address?overwrite)
    Nullables validation
 
 */