using System;
using System.Collections.Generic;

namespace upp.Entities;

public partial class QualityControl
{
    public int ControlId { get; set; }

    public int ProductId { get; set; }

    public int? ProductionId { get; set; }

    public DateTime CheckDate { get; set; }

    public int EmployeeId { get; set; }

    public string Status { get; set; } = null!;

    public string? DefectDescription { get; set; }

    public decimal QuantityChecked { get; set; }

    public decimal? QuantityRejected { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Finishedproduct Product { get; set; } = null!;

    public virtual Production? Production { get; set; }
}
