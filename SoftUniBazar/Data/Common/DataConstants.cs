namespace SoftUniBazar.Data.Common
{
    public static class DataConstants
    {
        // Ad constants:
        public const int AdNameMinLength = 5;
        public const int AdNameMaxLength = 25;

        public const int AdDescriptionMinLength = 15;
        public const int AdDescriptionMaxLength = 250;

        public const int AdImageUrlMaxLength = 255;

        public const string DateTimeFormat = "yyyy-MM-dd H:mm";

        // Category constants:
        public const int CategoryNameMinLength = 3;
        public const int CategoryNameMaxLength = 15;

        public const string CategoryInvalidMessage = "This category does not exist.";

        // Names of actions and controllers:
        public const string AllActionName = "All";
        public const string AdControllerName = "Ad";
    }
}
