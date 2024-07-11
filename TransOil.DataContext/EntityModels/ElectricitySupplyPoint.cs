using System.ComponentModel.DataAnnotations.Schema;

namespace TransOil.DataContext.EntityModels;

/// <summary>
/// Точка поставки электроэнергии
/// </summary>
public class ElectricitySupplyPoint
{
    public int SupplyPointId { get; set; }
    [Column(TypeName = "varchar(50)")]
    public string Name { get; set; } = null!;
    public double MaxVoltage { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
}
