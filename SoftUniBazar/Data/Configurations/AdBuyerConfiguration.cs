using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftUniBazar.Data.DataModels;

namespace SoftUniBazar.Data.Configurations
{
    public class AdBuyerConfiguration : IEntityTypeConfiguration<AdBuyer>
    {
        public void Configure(EntityTypeBuilder<AdBuyer> builder)
        {
            builder.HasKey(ab => new
            {
                ab.BuyerId,
                ab.AdId
            });

            builder.HasOne(ab => ab.Buyer)
                .WithMany()
                .HasForeignKey(a => a.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
