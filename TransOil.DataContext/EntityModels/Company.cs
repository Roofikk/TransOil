using System.ComponentModel.DataAnnotations.Schema;

namespace TransOil.DataContext.EntityModels;

public class Company : CompanyBase
{
    public IEnumerable<ChildCompany>? ChildCompanies { get; set; } = [];
}
