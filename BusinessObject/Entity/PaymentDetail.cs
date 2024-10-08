using System;
using System.Collections.Generic;

namespace BusinessObject.Entity;

public partial class PaymentDetail
{
    public int PaymentDetailId { get; set; }

    public int? PaymentId { get; set; }

    public string? Description { get; set; }

    public int? Status { get; set; }

    public virtual Payment? Payment { get; set; }
}
