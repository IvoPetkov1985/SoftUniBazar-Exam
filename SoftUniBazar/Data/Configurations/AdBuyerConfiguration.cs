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
                .HasForeignKey(ab => ab.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ab => ab.Ad)
                .WithMany()
                .HasForeignKey(ab => ab.AdId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
