using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SoftUniBazar.Data.Common.DataConstants;

namespace SoftUniBazar.Data.DataModels
{
    [Comment("The ad itself which a user could choose and add to his/her cart")]
    public class Ad
    {
        [Key]
        [Comment("Ad identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(AdNameMaximumLength)]
        [Comment("The name of the ad")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(AdDescriptionMaxLength)]
        [Comment("Detailed information about the ad")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Comment("The price of the product")]
        public decimal Price { get; set; }

        [Required]
        [Comment("Product owner identifier")]
        public string OwnerId { get; set; } = string.Empty;

        [ForeignKey(nameof(OwnerId))]
        public IdentityUser Owner { get; set; } = null!;

        [Required]
        [MaxLength(AdImageUrlMaxLength)]
        [Comment("The image address")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        [Comment("Date and time of creation of the ad")]
        public DateTime CreatedOn { get; set; }

        [Required]
        [Comment("Category identifier")]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;
    }
}
