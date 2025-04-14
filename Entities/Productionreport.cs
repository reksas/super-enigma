using System;
using System.Collections.Generic;

namespace upp.Entities;

public partial class Productionreport
{
    public int Productionreportid { get; set; }

    public DateOnly? Reportdate { get; set; }

    public int? Materialid { get; set; }

    public decimal? Quantityused { get; set; }

    public decimal? Quantityproduced { get; set; }

    public virtual Rawmaterial? Material { get; set; }
}
