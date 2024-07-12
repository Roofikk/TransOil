using System.Text.Json.Serialization;

namespace TransOil.WebApi.Dto.Counters;

public class CounterCreateDto : CounterDto
{
    [JsonIgnore]
    public override int Id { get; set; }
}
