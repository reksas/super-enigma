using System;
using System.Collections.Generic;

namespace upp.Entities;

public partial class CustomerOrder
{
    public int OrderId { get; set; }

    public string CustomerName { get; set; } = null!;

    public DateOnly OrderDate { get; set; }

    public DateOnly? DeliveryDate { get; set; }

    public string Status { get; set; } = null!;

    public string? ContactEmail { get; set; }

    public string ContactPhone { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
