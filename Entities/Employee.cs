using System;
using System.Collections.Generic;

namespace upp.Entities;

public partial class Employee
{
    public int Employeeid { get; set; }

    public string? Fullname { get; set; }

    public string? Position { get; set; }

    public string? Department { get; set; }

    public virtual ICollection<Production> Productions { get; set; } = new List<Production>();

    public virtual ICollection<QualityControl> QualityControls { get; set; } = new List<QualityControl>();
}
