using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftUniBazar.Data.DataModels;

namespace SoftUniBazar.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(new Category()
            {
                Id = 1,
                Name = "Books"
            },
            new Category()
            {
                Id = 2,
                Name = "Cars"
            },
            new Category()
            {
                Id = 3,
                Name = "Clothes"
            },
            new Category()
            {
                Id = 4,
                Name = "Home"
            },
            new Category()
            {
                Id = 5,
                Name = "Technology"
            },
            new Category()
            {
                Id = 6,
                Name = "Garden"
            },
            new Category()
            {
                Id = 7,
                Name = "Sport"
            },
            new Category()
            {
                Id = 8,
                Name = "Children"
            });
        }
    }
}
