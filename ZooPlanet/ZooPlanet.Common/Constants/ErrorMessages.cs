namespace ZooPlanet.Common.Constants
{
    public static class ErrorMessages
    {
        public const string AnimalNameMinLengthErrorMessage = "Animal name must be at least 2 symbols.";
        public const string AnimalNameMaxLengthErrorMessage = "Animal name cannot be more than 50 symbols.";

        public const string AnimalAgeErrorMessage = "Animal age must be between 0 and 50 years.";

        public const string TitleMinLengthErrorMessage = "Title cannot be less than 5 symbols.";
        public const string TitleMaxLengthErrorMessage = "Title cannot be more than 20 symbols.";

        public const string MessageMinLengthErrorMessage = "Message cannot be less than 10 symbols.";
        public const string MessageMaxLengthErrorMessage = "Message cannot be more than 200 symbols.";
    }
}
