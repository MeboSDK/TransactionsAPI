using Domain.Primitives;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities;
public class Transaction : Entity
{
    public int SenderUserId { get; set; }
    public int ReceiverUserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime TimestampUTC { get; set; }

    [ForeignKey("SenderUserId")]
    public virtual User Sender { get; set; }

    [ForeignKey("ReceiverUserId")]
    public virtual User Receiver { get; set; }
}
