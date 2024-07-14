using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Globalization;
using System.Net.Http.Headers;
using TransOil.DataContext;
using TransOil.DataContext.EntityModels;
using TransOil.WebApi.Dto.Counters;

namespace TransOil.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ILogger<CustomersController> _logger;
    private readonly TransOilContext _context;

    public CustomersController(ILogger<CustomersController> logger, TransOilContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> Get()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> Get(int id)
    {
        if (!await _context.Customers.AnyAsync(x => x.CustomerId == id))
        {
            return NotFound();
        }

        return Ok();
    }

    /// <summary>
    /// Запрос для задания 1.2.3-5. Получение списка счетчиков с закончившимся сроком поверки.<br/>
    /// Параметр запроса: ?types=Electricity,Current,Voltage где значения типов счетчиков задаются через запятую
    /// </summary>
    /// <param name="id"></param>
    /// <param name="types"></param>
    /// <returns></returns>
    [HttpGet("{id}/get-expired-counters")]
    public async Task<ActionResult<IEnumerable<TransformerCounterRetrieveDto>>> GetExpiredCounters(int id,
        [FromQuery(Name = "types")] string? types)
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

        return Ok(MapCounters(await query.ToListAsync()));
    }

    /// <summary>
    /// Задание 1.2.3. Поиск всех счетчиков с закончившимся сроком поверки
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}/get-all-expired-counters")]
    public async Task<ActionResult<IEnumerable<TransformerCounterRetrieveDto>>> GetAllExpiredCounters(int id)
    {
        return Ok(await GetExpiredCounters(id, null));
    }

    /// <summary>
    /// Задание 1.2.4. Поиск трансформаторов напряжения с закончившимся сроком поверки
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}/get-expired-voltage-counters")]
    public async Task<ActionResult<IEnumerable<TransformerCounterRetrieveDto>>> GetExpiredVoltageCounters(int id)
    {
        return Ok(await GetExpiredCounters(id, "Voltage"));
    }

    /// <summary>
    /// Задание 1.2.5. Поиск трансформаторов тока с закончившимся сроком поверки
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}/get-expired-current-counters")]
    public async Task<ActionResult<IEnumerable<TransformerCounterRetrieveDto>>> GetExpiredCurrentCounters(int id)
    {
        return Ok(await GetExpiredCounters(id, "Current"));
    }

    private TransformerCounterRetrieveDto MapCounter(CounterBase counter)
    {
        return new TransformerCounterRetrieveDto
        {
            Id = counter.Id,
            Number = counter.Number,
            Type = counter.Type,
            VerifyDate = counter.VerifyDate,
            TransformerRatio = counter is TransformerCounterBase currentTransformer
                ? currentTransformer.TransformerRatio
                : null
        };
    }

    private IEnumerable<CounterRetrieveDto> MapCounters(IEnumerable<CounterBase> counters)
    {
        return counters.Select(MapCounter);
    }
}

public enum CounterType
{
    Electricity,
    Current,
    Voltage,
}
