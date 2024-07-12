using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransOil.DataContext;
using TransOil.DataContext.EntityModels;

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
    public async Task<ActionResult<IEnumerable<MeasurementDevice>>> Get()
    {
        return await _context.MeasurementDevices.AsNoTracking().ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MeasurementDevice?>> Get(int id)
    {
        return await _context.MeasurementDevices.AsNoTracking().FirstOrDefaultAsync(x => x.DeviceId == id);
    }
}
