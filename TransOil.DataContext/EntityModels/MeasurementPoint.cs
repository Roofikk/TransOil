using System.ComponentModel.DataAnnotations.Schema;

namespace TransOil.DataContext.EntityModels;

/// <summary>
/// Точка измерения электроэнергии
/// </summary>
public class MeasurementPoint
{
    public int MeasurementPointId { get; set; }
    [Column(TypeName = "varchar(50)")]
    public string Name { get; set; } = null!;

    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; } = null!;

    public virtual ElectricEnergyCounter ElectricityCounter { get; set; } = null!;
    public virtual CurrentTransformer CurrentTransformer { get; set; } = null!;
    public virtual VoltageTransformer VoltageTransformer { get; set; } = null!;

    public IEnumerable<Measurement> Measurements { get; set; } = [];
    public IEnumerable<MeasurementDevice> Devices { get; set; } = [];
}
