using System.Text.Json.Serialization;
using TransOil.WebApi.Dto.Counters;

namespace TransOil.WebApi.Dto.MeasurementPoints;

public class MeasurementPointCreateDto : IMeasurementPointDto
{
    [JsonIgnore]
    public int MeasurementPointId { get; set; }
    public string Name { get; set; } = null!;
    public int CustomerId { get; set; }
    public CounterCreateDto? ElectricityCounter { get; set; }
    public TransformerCounterCreateDto? CurrentTransformerCounter { get; set; }
    public TransformerCounterCreateDto? VoltageTransformerCounter { get; set; }
}
