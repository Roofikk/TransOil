using System.ComponentModel.DataAnnotations.Schema;

namespace TransOil.DataContext.EntityModels;

/// <summary>
/// Точка измерения электроэнергии
/// </summary>
public class MeasurementPoint
{
    public int MeasurementPointId { get; set; }
    [Column(TypeName = "varchar(100)")]
    public string Name { get; set; } = null!;

    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; } = null!;

    public virtual ElectricityCounter? ElectricityCounter { get; set; }
    public virtual CurrentTransformer? CurrentTransformer { get; set; }
    public virtual VoltageTransformer? VoltageTransformer { get; set; }

    public virtual ICollection<Measurement> Measurements { get; set; } = [];
}
