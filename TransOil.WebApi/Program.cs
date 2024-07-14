using TransOil.DataContext;
using TransOil.DataContext.DataFiller;
using TransOil.WebApi.Services.Customers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransOilContext(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddScoped<ICustomersService, CustomersService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

if (builder.Configuration.GetValue<bool>("DataRandomFiller:Enabled"))
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<TransOilContext>();
        await dbContext.InitialAsync();
        await dbContext.SeedDataAsync();
    }
}

app.Run();
