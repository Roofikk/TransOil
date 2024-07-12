using System.Text.Json.Serialization;

namespace TransOil.WebApi.Dto.Counters;

public class TransformerCounterCreateDto : TransformerCounterDto
{
    [JsonIgnore]
    public override int Id { get; set; }
}
