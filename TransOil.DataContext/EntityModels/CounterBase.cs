using System.ComponentModel.DataAnnotations.Schema;

namespace TransOil.DataContext.EntityModels;

public class CounterBase
{
    public virtual int Id { get; set; }
    public virtual int Number { get; set; }
    [Column(TypeName = "nvarchar(50)")]
    public virtual string Type { get; set; } = null!;
    public virtual DateTime VerifyDate { get; set; }

    public int MeasurementPointId { get; set; }
    public virtual MeasurementPoint MeasurementPoint { get; set; } = null!;
}
