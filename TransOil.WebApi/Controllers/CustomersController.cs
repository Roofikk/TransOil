using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransOil.DataContext;
using TransOil.DataContext.EntityModels;
using TransOil.WebApi.Dto.Counters;
using TransOil.WebApi.Services.Customers;

namespace TransOil.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ILogger<CustomersController> _logger;
    private readonly ICustomersService _customersService;

    public CustomersController(ILogger<CustomersController> logger, ICustomersService customersService)
    {
        _logger = logger;
        _customersService = customersService;
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
        return Ok(MapCounters(await _customersService.GetExpiredCountersOfCustomer(id, types)));
    }

    /// <summary>
    /// Задание 1.2.3. Поиск всех счетчиков с закончившимся сроком поверки
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}/get-all-expired-counters")]
    public async Task<ActionResult<IEnumerable<TransformerCounterRetrieveDto>>> GetAllExpiredCounters(int id)
    {
        return Ok(MapCounters(await _customersService.GetExpiredCountersOfCustomer(id, null)));
    }

    /// <summary>
    /// Задание 1.2.4. Поиск трансформаторов напряжения с закончившимся сроком поверки
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}/get-expired-voltage-counters")]
    public async Task<ActionResult<IEnumerable<TransformerCounterRetrieveDto>>> GetExpiredVoltageCounters(int id)
    {
        return Ok(MapCounters(await _customersService.GetExpiredCountersOfCustomer(id, "Voltage")));
    }

    /// <summary>
    /// Задание 1.2.5. Поиск трансформаторов тока с закончившимся сроком поверки
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}/get-expired-current-counters")]
    public async Task<ActionResult<IEnumerable<TransformerCounterRetrieveDto>>> GetExpiredCurrentCounters(int id)
    {
        return Ok(MapCounters(await _customersService.GetExpiredCountersOfCustomer(id, "Current")));
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
