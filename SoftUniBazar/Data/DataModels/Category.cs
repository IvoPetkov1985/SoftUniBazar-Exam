using System.ComponentModel.DataAnnotations;
using static SoftUniBazar.Data.Common.DataConstants;

namespace SoftUniBazar.Data.DataModels
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaximumLength)]
        public string Name { get; set; } = string.Empty;

        public IEnumerable<Ad> Ads { get; set; } = new List<Ad>();
    }
}
