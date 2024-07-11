using System.ComponentModel.DataAnnotations.Schema;

namespace TransOil.DataContext.EntityModels;

/// <summary>
/// Расчетный прибор учета
/// </summary>
public class MeasurementDevice
{
    public int DeviceId { get; set; }
    [Column(TypeName = "varchar(50)")]
    public string Name { get; set; } = null!;

    public IEnumerable<Measurement> Measurements { get; set; } = [];
    public IEnumerable<MeasurementPoint> MeasurementPoints { get; set; } = [];
}
