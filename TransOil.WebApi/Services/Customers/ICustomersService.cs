using TransOil.DataContext.EntityModels;

namespace TransOil.WebApi.Services.Customers;

public interface ICustomersService
{
    public Task<IEnumerable<CounterBase>> GetExpiredCountersOfCustomer(int id, string? types);
}
