using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Transaction : Entity
{
    public int SenderUserId { get; set; }
    public int ReceiverUserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Timestamp { get; set; }

    [ForeignKey("SenderUserId")]
    public virtual User Sender { get; set; }

    [ForeignKey("ReceiverUserId")]
    public virtual User Receiver { get; set; }
}
