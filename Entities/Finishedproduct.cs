using System;
using System.Collections.Generic;

namespace upp.Entities;

public partial class Finishedproduct
{
    public int Productid { get; set; }

    public string? Productname { get; set; }

    public decimal? Quantity { get; set; }

    public string? Unit { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<QualityControl> QualityControls { get; set; } = new List<QualityControl>();

    public virtual ICollection<Shipment> Shipments { get; set; } = new List<Shipment>();

    public virtual ICollection<StorageReport> StorageReports { get; set; } = new List<StorageReport>();

    public virtual ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
}
