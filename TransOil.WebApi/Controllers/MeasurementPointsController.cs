using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransOil.DataContext;
using TransOil.DataContext.EntityModels;
using TransOil.WebApi.Dto.Counters;
using TransOil.WebApi.Dto.MeasurementPoints;

namespace TransOil.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MeasurementPointsController : ControllerBase
{
    private readonly ILogger<MeasurementPointsController> _logger;
    private readonly TransOilContext _context;

    public MeasurementPointsController(ILogger<MeasurementPointsController> logger, TransOilContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MeasurementPointRetrieveDto>>> Get()
    {
        return Ok(MapMeasurementPoints(await _context.MeasurementPoints
            .Include(x => x.ElectricityCounter)
            .Include(x => x.CurrentTransformer)
            .Include(x => x.VoltageTransformer)
            .ToListAsync()));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MeasurementPointRetrieveDto>> Get(int id)
    {
        if (!await _context.MeasurementPoints.AnyAsync(x => x.MeasurementPointId == id))
        {
            return NotFound();
        }

        return MapMeasurementPoint(await _context.MeasurementPoints
            .Include(x => x.ElectricityCounter)
            .Include(x => x.CurrentTransformer)
            .Include(x => x.VoltageTransformer)
            .FirstAsync(x => x.MeasurementPointId == id));
    }

    /// <summary>
    /// Задание 1.2.1. Добавление новой точки измерения с указаниями всех счетчиков
    /// </summary>
    /// <param name="measurementPoint"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<MeasurementPointRetrieveDto?>> Create([FromBody] MeasurementPointCreateDto measurementPoint)
    {
        if (!await _context.Customers.AnyAsync(x => x.CustomerId == measurementPoint.CustomerId))
        {
            return NotFound("Customer not found");
        }

        var measurementPointEntity = new MeasurementPoint
        {
            Name = measurementPoint.Name,
            CustomerId = measurementPoint.CustomerId
        };

        if (measurementPoint.ElectricityCounter != null)
        {
            measurementPointEntity.ElectricityCounter = new ElectricityCounter
            {
                Number = measurementPoint.ElectricityCounter.Number,
                Type = measurementPoint.ElectricityCounter.Type,
                VerifyDate = measurementPoint.ElectricityCounter.VerifyDate,
            };
        }

        if (measurementPoint.CurrentTransformerCounter != null)
        {
            measurementPointEntity.CurrentTransformer = new CurrentTransformer
            {
                Number = measurementPoint.CurrentTransformerCounter.Number,
                Type = measurementPoint.CurrentTransformerCounter.Type,
                VerifyDate = measurementPoint.CurrentTransformerCounter.VerifyDate,
                TransformerRatio = measurementPoint.CurrentTransformerCounter.TransformerRatio!.Value
            };
        }

        if (measurementPoint.VoltageTransformerCounter != null)
        {
            measurementPointEntity.VoltageTransformer = new VoltageTransformer
            {
                Number = measurementPoint.VoltageTransformerCounter.Number,
                Type = measurementPoint.VoltageTransformerCounter.Type,
                VerifyDate = measurementPoint.VoltageTransformerCounter.VerifyDate,
                TransformerRatio = measurementPoint.VoltageTransformerCounter.TransformerRatio!.Value
            };
        }

        var entry = await _context.MeasurementPoints.AddAsync(measurementPointEntity);
        await _context.SaveChangesAsync();
        return MapMeasurementPoint(entry.Entity);
    }

    private MeasurementPointRetrieveDto MapMeasurementPoint(MeasurementPoint measurementPoint)
    {
        var retrieveDto = new MeasurementPointRetrieveDto
        {
            MeasurementPointId = measurementPoint.MeasurementPointId,
            Name = measurementPoint.Name,
            CustomerId = measurementPoint.CustomerId
        };

        if (measurementPoint.ElectricityCounter != null)
        {
            retrieveDto.ElectricityCounter = new CounterRetrieveDto
            {
                Id = measurementPoint.ElectricityCounter.Id,
                Number = measurementPoint.ElectricityCounter.Number,
                Type = measurementPoint.ElectricityCounter.Type,
                VerifyDate = measurementPoint.ElectricityCounter.VerifyDate
            };
        }

        if (measurementPoint.CurrentTransformer != null)
        {
            retrieveDto.CurrentTransformer = new TransformerCounterRetrieveDto
            {
                Id = measurementPoint.CurrentTransformer.Id,
                Number = measurementPoint.CurrentTransformer.Number,
                Type = measurementPoint.CurrentTransformer.Type,
                VerifyDate = measurementPoint.CurrentTransformer.VerifyDate,
                TransformerRatio = measurementPoint.CurrentTransformer.TransformerRatio
            };
        }

        if (measurementPoint.VoltageTransformer != null)
        {
            retrieveDto.VoltageTransformer = new TransformerCounterRetrieveDto
            {
                Id = measurementPoint.VoltageTransformer.Id,
                Number = measurementPoint.VoltageTransformer.Number,
                Type = measurementPoint.VoltageTransformer.Type,
                VerifyDate = measurementPoint.VoltageTransformer.VerifyDate,
                TransformerRatio = measurementPoint.VoltageTransformer.TransformerRatio
            };
        }

        return retrieveDto;
    }

    private IEnumerable<MeasurementPointRetrieveDto> MapMeasurementPoints(IEnumerable<MeasurementPoint> measurementPoints)
    {
        return measurementPoints.Select(MapMeasurementPoint);
    }
}
