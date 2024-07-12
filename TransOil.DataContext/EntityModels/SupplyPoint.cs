using System.ComponentModel.DataAnnotations.Schema;

namespace TransOil.DataContext.EntityModels;

/// <summary>
/// Точка поставки электроэнергии
/// </summary>
public class SupplyPoint
{
    public int SupplyPointId { get; set; }
    [Column(TypeName = "varchar(100)")]
    public string Name { get; set; } = null!;
    public double MaxVoltage { get; set; }

    public MeasurementDevice MeasurementDevice { get; set; } = null!;

    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
}
