using System.ComponentModel.DataAnnotations.Schema;

namespace TransOil.DataContext.EntityModels;

public abstract class CompanyBase
{
    public virtual int CompanyId { get; set; }
    [Column(TypeName = "varchar(128)")]
    public virtual string Name { get; set; } = null!;
    [Column(TypeName = "varchar(255)")]
    public virtual string Address { get; set; } = null!;
}
