using System;
using System.Collections.Generic;

namespace upp.Entities;

public partial class Shipment
{
    public int Shipmentid { get; set; }

    public int? Productid { get; set; }

    public decimal? Quantityshipped { get; set; }

    public DateOnly? Shipmentdate { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Finishedproduct? Product { get; set; }
}
