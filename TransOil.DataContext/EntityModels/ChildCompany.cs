namespace TransOil.DataContext.EntityModels;

public class ChildCompany : CompanyBase
{
    public int ParentCompanyId { get; set; }
    public Company Company { get; set; } = null!;
}
