using System.ComponentModel.DataAnnotations.Schema;

namespace TransOil.DataContext.EntityModels;

/// <summary>
/// Организация
/// </summary>
public class Company : CompanyBase
{
    public IEnumerable<ChildCompany>? ChildCompanies { get; set; } = [];
}
