using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransOil.DataContext;
using TransOil.WebApi.Dto.MeasurementDevices;

namespace TransOil.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MeasurementDevicesController : ControllerBase
{
    private readonly ILogger<MeasurementDevicesController> _logger;
    private readonly TransOilContext _context;

    public MeasurementDevicesController(ILogger<MeasurementDevicesController> logger, TransOilContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MeasurementDeviceDto>>> Get([FromQuery] TimeInterval timeInterval)
    {
        var query = _context.MeasurementDevices.SelectMany(x => x.Measurements
            .Where(y => y.Date >= timeInterval.DateFrom && y.Date <= timeInterval.DateTo),
                (x, y) => new MeasurementDeviceDto
                {
                    DeviceId = x.DeviceId,
                    Name = x.Name,
                    SupplyPointId = x.SupplyPointId,
                    MeasurementDate = y.Date,
                });

        return Ok(await query.ToListAsync());
    }

    /// <summary>
    /// Задание 1.2.2. Получение списка измерительных устройств за 2018 год
    /// </summary>
    /// <returns></returns>
    [HttpGet("get2018")]
    public async Task<ActionResult<IEnumerable<MeasurementDeviceDto>>> Get()
    {
        return await Get(new TimeInterval()
        {
            DateFrom = new DateTime(2018, 1, 1),
            DateTo = new DateTime(2018, 12, 31, 23, 59, 59)
        });
    }
}

public class TimeInterval
{
    [FromQuery(Name = "from")]
    public DateTime? DateFrom { get; set; }
    [FromQuery(Name = "to")]
    public DateTime? DateTo { get; set; }
}
