namespace TransOil.DataContext.EntityModels;

/// <summary>
/// Дочерняя организация
/// </summary>
public class ChildCompany : CompanyBase
{
    public int ParentCompanyId { get; set; }
    public Company ParentCompany { get; set; } = null!;

    public virtual ICollection<Customer> Customers { get; set; } = [];
}
