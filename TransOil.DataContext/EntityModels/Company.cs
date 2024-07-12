using System.ComponentModel.DataAnnotations.Schema;

namespace TransOil.DataContext.EntityModels;

/// <summary>
/// Организация
/// </summary>
public class Company : CompanyBase
{
    public ICollection<ChildCompany> ChildCompanies { get; set; } = [];
}
