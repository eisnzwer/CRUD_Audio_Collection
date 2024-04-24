using CRUD_Audio_Collection.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRUD_Audio_Collection.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        
        builder.HasOne(u => u.PaymentData)
            .WithOne(pd => pd.User)
            .HasForeignKey<PaymentData>(pd => pd.UserId);
    }
}