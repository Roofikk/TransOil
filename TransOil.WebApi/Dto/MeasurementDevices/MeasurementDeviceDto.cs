namespace TransOil.WebApi.Dto.MeasurementDevices;

public class MeasurementDeviceDto
{
    public virtual int DeviceId { get; set; }
    public virtual string Name { get; set; } = null!;
    public virtual int SupplyPointId { get; set; }

    public virtual DateTime MeasurementDate { get; set; }
}
