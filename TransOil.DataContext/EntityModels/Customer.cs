using System.ComponentModel.DataAnnotations.Schema;

namespace TransOil.DataContext.EntityModels;

/// <summary>
/// Объект потребления
/// </summary>
public class Customer
{
    public int CustomerId { get; set; }
    [Column(TypeName = "varchar(50)")]
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;

    public int CompanyId { get; set; }
    public ChildCompany Company { get; set; } = null!;

    public IEnumerable<MeasurementPoint> Measurments { get; set; } = [];
    public IEnumerable<ElectricitySupplyPoint> Supplies { get; set; } = [];
}
