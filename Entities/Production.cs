using System;
using System.Collections.Generic;

namespace upp.Entities;

public partial class Production
{
    public int Productionid { get; set; }

    public int? Materialid { get; set; }

    public int? Employeeid { get; set; }

    public DateOnly? Productiondate { get; set; }

    public decimal? Quantityproduced { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Rawmaterial? Material { get; set; }

    public virtual ICollection<QualityControl> QualityControls { get; set; } = new List<QualityControl>();
}
