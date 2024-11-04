namespace SoftUniBazar.Data.Common
{
    public static class DataConstants
    {
        public const int AdNameMinimumLength = 5;
        public const int AdNameMaximumLength = 25;

        public const int AdDescriptionMinimumLength = 15;
        public const int AdDescriptionMaximumLength = 250;

        public const string DateTimeFormat = "yyyy-MM-dd H:mm";

        public const int CategoryNameMinimumLength = 3;
        public const int CategoryNameMaximumLength = 15;

        public const string MissingCategoryErrorMsg = "Category does not exist.";
    }
}
