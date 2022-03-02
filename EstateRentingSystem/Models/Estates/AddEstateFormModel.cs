namespace EstateRentingSystem.Models.Estates
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;
    public class AddEstateFormModel
    {
        [Required]
        [StringLength(EstateTypeMaxLength, MinimumLength = EstateTypeMinLength)]
        public string Type { get; init; }

        [Display(Name = "Type of construction")]
        [Required]
        [StringLength(EstateTypeOfConstructionMaxLength, MinimumLength = EstateTypeOfConstructionMinLength)]
        public string TypeOfConstruction { get; init; }

        [Required]
        [StringLength(
            int.MaxValue,
            MinimumLength = EstateDescriptionMinLength, 
            ErrorMessage = "The field Description must be a string with a minimum length of {2}.")]
        public string Description { get; init; }

        [Display(Name = "Year of construction")]
        [Range(EstateYearOfConstructionMinValue, EstateYearOfConstructionMaxValue)]
        public int YearOfConstruction { get; init; }

        [Range(EstateSquaringMinValue, EstateSquaringMaxValue)]
        public int Squaring { get; init; }

        [Display(Name = "Image URL")]
        [Required]
        [Url]
        public string ImageUrl { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<EstateCategoryViewModel> Categories { get; set; }
    }
}
