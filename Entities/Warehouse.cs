using System;
using System.Collections.Generic;

namespace upp.Entities;

public partial class Warehouse
{
    public int Warehouseid { get; set; }

    public int? Productid { get; set; }

    public decimal? Quantitystored { get; set; }

    public DateOnly? Storagedate { get; set; }

    public virtual Finishedproduct? Product { get; set; }
}
