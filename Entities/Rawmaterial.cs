using System;
using System.Collections.Generic;

namespace upp.Entities;

public partial class Rawmaterial
{
    public int Materialid { get; set; }

    public string? Materialname { get; set; }

    public decimal? Quantity { get; set; }

    public string? Unit { get; set; }

    public virtual ICollection<Productionreport> Productionreports { get; set; } = new List<Productionreport>();

    public virtual ICollection<Production> Productions { get; set; } = new List<Production>();
}
