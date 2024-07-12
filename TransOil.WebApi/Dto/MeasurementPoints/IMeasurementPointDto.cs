using TransOil.DataContext.EntityModels;
using TransOil.WebApi.Dto.Counters;

namespace TransOil.WebApi.Dto.MeasurementPoints;

public interface IMeasurementPointDto
{
    public int MeasurementPointId { get; set; }
    public string Name { get; set; }
    public int CustomerId { get; set; }
}
