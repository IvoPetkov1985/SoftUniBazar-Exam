namespace SoftUniBazar.Data.Common
{
    public static class DataConstants
    {
        // Ad constants:
        public const int AdNameMinimumLength = 5;
        public const int AdNameMaximumLength = 25;

        public const int AdDescriptionMinLength = 15;
        public const int AdDescriptionMaxLength = 250;

        public const int AdImageUrlMaxLength = 250;

        public const string AdDateAndTimeFormat = "yyyy-MM-dd H:mm";

        // Category constants:
        public const int CategoryNameMinLength = 3;
        public const int CategoryNameMaxLength = 15;

        public const string CategoryMissingMsg = "This category does not exist.";
    }
}
