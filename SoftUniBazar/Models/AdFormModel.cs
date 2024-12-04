using System.ComponentModel.DataAnnotations;
using static SoftUniBazar.Data.Common.DataConstants;

namespace SoftUniBazar.Models
{
    public class AdFormModel
    {
        [Required]
        [StringLength(AdNameMaxLength, MinimumLength = AdNameMinLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(AdDescriptionMaxLength, MinimumLength = AdDescriptionMinLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(AdImageUrlMaxLength)]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        [Range(0.00, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}

//•	Has Id – a unique integer, Primary Key
//•	Has Name – a string with min length 5 and max length 25 (required)
//•	Has Description – a string with min length 15 and max length 250 (required)
//•	Has Price – a decimal (required)
//•	Has OwnerId – a string (required)
//•	Has Owner – an IdentityUser (required)
//•	Has ImageUrl – a string (required)
//•	Has CreatedOn – a DateTime with format "yyyy-MM-dd H:mm" (required) (the DateTime format is recommended, if you are having troubles with this one, you are free to use another one)
//•	Has CategoryId – an integer, foreign key (required)
//•	Has Category – a Category (required)
