using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;

namespace TransOil.DataContext;

public static class TransOilContextExtension
{
    public static IServiceCollection AddTransOilContext(
        this IServiceCollection services,
        string? connectionString = null)
    {
        connectionString ??= new MySqlConnectionStringBuilder()
        {
            Server = "localhost",
            Port = 3306,
            Database = "trans_oil",
            UserID = "root",
            Password = "root1234"
        }.ConnectionString;

        return services.AddDbContext<TransOilContext>(options =>
        {
            options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString),
                options =>
                {
                    options.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                });
        });
    }
}
