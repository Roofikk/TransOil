
namespace TransOil.WebApi.Dto.Counters;

public class CounterDto
{
    public virtual int Id { get; set; }
    public virtual int Number { get; set; }
    public virtual string Type { get; set; } = null!;
    public virtual DateTime VerifyDate { get; set; }
}
