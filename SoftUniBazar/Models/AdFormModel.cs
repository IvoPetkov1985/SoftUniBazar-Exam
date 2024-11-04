using System.ComponentModel.DataAnnotations;
using static SoftUniBazar.Data.Common.DataConstants;

namespace SoftUniBazar.Models
{
    public class AdFormModel
    {
        [Required]
        [StringLength(AdNameMaximumLength, MinimumLength = AdNameMinimumLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(AdDescriptionMaximumLength, MinimumLength = AdDescriptionMinimumLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}
