using System.Text.Json.Serialization;

namespace TransOil.WebApi.Dto.Counters;

public class TransformerCounterCreateDto : CounterCreateDto
{
    public double? TransformerRatio { get; set; }
}
