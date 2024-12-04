using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SoftUniBazar.Data.Common.DataConstants;

namespace SoftUniBazar.Data.DataModels
{
    [Comment("An ad from SoftUni Bazar platform")]
    public class Ad
    {
        [Key]
        [Comment("Ad identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(AdNameMaxLength)]
        [Comment("Ad name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(AdDescriptionMaxLength)]
        [Comment("Details about the ad")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Comment("The price of the presented product")]
        public decimal Price { get; set; }

        [Required]
        [Comment("Owner (publisher) identifier")]
        public string OwnerId { get; set; } = string.Empty;

        [ForeignKey(nameof(OwnerId))]
        public IdentityUser Owner { get; set; } = null!;

        [Required]
        [MaxLength(AdImageUrlMaxLength)]
        [Comment("The photo of the product")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        [Comment("Date and time of the ad creation")]
        public DateTime CreatedOn { get; set; }

        [Required]
        [Comment("Category identifier")]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;
    }
}
