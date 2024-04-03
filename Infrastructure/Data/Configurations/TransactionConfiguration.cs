using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasOne(t => t.Sender)
        .WithMany()
        .HasForeignKey(t => t.SenderUserId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Receiver)
         .WithMany()
         .HasForeignKey(t => t.ReceiverUserId)
         .OnDelete(DeleteBehavior.Restrict);
    }
}
