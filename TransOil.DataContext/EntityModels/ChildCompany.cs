namespace TransOil.DataContext.EntityModels;

/// <summary>
/// Дочерняя организация
/// </summary>
public class ChildCompany : CompanyBase
{
    public int ParentCompanyId { get; set; }
    public Company Company { get; set; } = null!;

    public IEnumerable<Customer> Customers { get; set; } = [];
}
