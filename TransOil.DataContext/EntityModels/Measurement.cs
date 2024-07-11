namespace TransOil.DataContext.EntityModels;

public class Measurement
{
    public int MeasurementId { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;

    public int MeasurementPointId { get; set; }
    public int MeasurementDeviceId { get; set; }
    public virtual MeasurementPoint MeasurementPoint { get; set; } = null!;
    public virtual MeasurementDevice MeasurementDevice { get; set; } = null!;
}
