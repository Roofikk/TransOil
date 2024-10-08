﻿using System.ComponentModel.DataAnnotations.Schema;

namespace TransOil.DataContext.EntityModels;

/// <summary>
/// Расчетный прибор учета
/// </summary>
public class MeasurementDevice
{
    public int DeviceId { get; set; }
    [Column(TypeName = "varchar(100)")]
    public string Name { get; set; } = null!;

    public int SupplyPointId { get; set; }
    public virtual SupplyPoint SupplyPoint { get; set; } = null!;

    public virtual ICollection<Measurement> Measurements { get; set; } = [];
}
