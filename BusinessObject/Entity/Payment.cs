namespace BusinessObject.Entity;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int? BookingId { get; set; }

    public int? UserId { get; set; }

    public decimal? Amount { get; set; }

    public DateTime? PaymentDate { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual ICollection<PaymentDetail> PaymentDetails { get; set; } = new List<PaymentDetail>();

    public virtual User? User { get; set; }
}