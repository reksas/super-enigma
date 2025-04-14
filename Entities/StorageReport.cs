using System;
using System.Collections.Generic;

namespace upp.Entities;

public partial class StorageReport
{
    public int Reportid { get; set; }

    public DateOnly? Reportdate { get; set; }

    public int? Productid { get; set; }

    public decimal? Quantityinstock { get; set; }

    public virtual Finishedproduct? Product { get; set; }
}
