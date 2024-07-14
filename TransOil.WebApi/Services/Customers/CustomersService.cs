using Microsoft.EntityFrameworkCore;
using TransOil.DataContext;
using TransOil.DataContext.EntityModels;
using TransOil.WebApi.Controllers;

namespace TransOil.WebApi.Services.Customers;

public class CustomersService : ICustomersService
{
    private readonly TransOilContext _context;

    public CustomersService(TransOilContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CounterBase>> GetExpiredCountersOfCustomer(int id, string? types)
    {
        var counterValues = types?.Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => Enum.TryParse(typeof(CounterType), s.Trim(), ignoreCase: true, out var result) ? result : null)
            .Where(r => r != null)
            .Cast<CounterType>()
            .Select(x => x.ToString())
            .ToList() ?? [];

        var query = _context.Counters.Where(x =>
            (x is ElectricityCounter && ((ElectricityCounter)x).MeasurementPoint.CustomerId == id) ||
            (x is CurrentTransformer && ((CurrentTransformer)x).MeasurementPoint.CustomerId == id) ||
            (x is VoltageTransformer && ((VoltageTransformer)x).MeasurementPoint.CustomerId == id));

        // выбрал промежуток истечения срока поверки по средним значениям счетчиков в течение 5 лет
        query = query.Where(x => x.VerifyDate.AddYears(5) < DateTime.Now);

        if (counterValues.Count > 0)
        {
            query = query.Where(x => counterValues.Contains(x.Discriminator));
        }

        return await query.ToListAsync();
    }
}
