using System.ComponentModel.DataAnnotations.Schema;

namespace TransOil.DataContext.EntityModels;

/// <summary>
/// Объект потребления
/// </summary>
public class Customer
{
    public int CustomerId { get; set; }
    [Column(TypeName = "varchar(100)")]
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;

    public int CompanyId { get; set; }
    public ChildCompany Company { get; set; } = null!;

    public ICollection<MeasurementPoint> Measurments { get; set; } = [];
    public ICollection<SupplyPoint> Supplies { get; set; } = [];
}
