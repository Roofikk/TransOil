using System.ComponentModel.DataAnnotations.Schema;

namespace TransOil.DataContext.EntityModels;

public abstract class CounterBase
{
    public virtual int Id { get; set; }
    public virtual int Number { get; set; }
    [Column(TypeName = "varchar(100)")]
    public virtual string Type { get; set; } = null!;
    public virtual DateTime VerifyDate { get; set; }
    [Column(TypeName = "varchar(11)")]
    public string Discriminator { get; set; } = null!;

    public int MeasurementPointId { get; set; }
    public virtual MeasurementPoint MeasurementPoint { get; set; } = null!;
}
