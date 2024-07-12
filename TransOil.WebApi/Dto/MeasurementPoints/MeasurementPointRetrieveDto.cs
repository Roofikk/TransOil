using System.Text.Json.Serialization;
using TransOil.DataContext.EntityModels;
using TransOil.WebApi.Dto.Counters;

namespace TransOil.WebApi.Dto.MeasurementPoints;

public class MeasurementPointRetrieveDto : IMeasurementPointDto
{
    public int MeasurementPointId { get; set; }
    public string Name { get; set; } = null!;
    public int CustomerId { get; set; }
    public CounterRetrieveDto? ElectricityCounter { get; set; }
    public TransformerCounterRetrieveDto? CurrentTransformer { get; set; }
    public TransformerCounterRetrieveDto? VoltageTransformer { get; set; }
}
