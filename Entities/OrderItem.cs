using System;
using System.Collections.Generic;

namespace upp.Entities;

public partial class OrderItem
{
    public int ItemId { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public decimal Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public int? ShipmentId { get; set; }

    public virtual CustomerOrder Order { get; set; } = null!;

    public virtual Finishedproduct Product { get; set; } = null!;

    public virtual Shipment? Shipment { get; set; }
}
