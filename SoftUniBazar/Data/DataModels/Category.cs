using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static SoftUniBazar.Data.Common.DataConstants;

namespace SoftUniBazar.Data.DataModels
{
    [Comment("The specific category of a given ad")]
    public class Category
    {
        [Key]
        [Comment("Category identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        [Comment("Category name")]
        public string Name { get; set; } = string.Empty;

        public IEnumerable<Ad> Ads { get; set; } = new List<Ad>();
    }
}
