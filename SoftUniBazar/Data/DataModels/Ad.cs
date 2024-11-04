﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SoftUniBazar.Data.Common.DataConstants;

namespace SoftUniBazar.Data.DataModels
{
    public class Ad
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(AdNameMaximumLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(AdDescriptionMaximumLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string OwnerId { get; set; } = string.Empty;

        [ForeignKey(nameof(OwnerId))]
        public IdentityUser Owner { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;
    }
}
