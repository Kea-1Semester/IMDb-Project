
namespace EfCoreModelsLib.DTO
{
    public class TitlesDto : IObjectId
    {
        public required string TitleType { get; set; }
        public required string PrimaryTitle { get; set; }
        public required string OriginalTitle { get; set; }
        public bool IsAdult { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public int RuntimeMinutes { get; set; }
        private DateOnly _year = DateOnly.FromDateTime(DateTime.Now);

        public void ValidateTitleType()
        {
            if (string.IsNullOrWhiteSpace(TitleType) && string.IsNullOrEmpty(TitleType))
            {
                throw new ArgumentNullException(nameof(TitleType), "TitleType cannot be null or empty.");
            }
            if (TitleType.Length is < 5 or > 25 || !TitleType.All(char.IsLetterOrDigit))
            {
                throw new ArgumentException("TitleType must be between 5 and 25 characters long and contain only letters.");
            }
        }

        public void ValidatePrimaryTitle()
        {
            if (string.IsNullOrWhiteSpace(PrimaryTitle) && string.IsNullOrEmpty(PrimaryTitle))
            {
                throw new ArgumentNullException(nameof(PrimaryTitle), "PrimaryTitle cannot be null or empty.");
            }
            if (PrimaryTitle.Length is < 5 or > 255 && !PrimaryTitle.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || c == '-' || c == '\'' || c == ','))
            {
                throw new ArgumentException("PrimaryTitle must be between 5 and 255 characters long and contain only letters, spaces, hyphens, apostrophes, or commas.");
            }
        }
        public void ValidateOriginalTitle()
        {
            if (string.IsNullOrWhiteSpace(OriginalTitle) && string.IsNullOrEmpty(OriginalTitle))
            {
                throw new ArgumentNullException(nameof(OriginalTitle), "OriginalTitle cannot be null or empty.");
            }
            if (OriginalTitle.Length is < 5 or > 255 && !OriginalTitle.All(c => char.IsLetter(c) || char.IsWhiteSpace(c) || c == '-' || c == '\'' || c == ','))
            {
                throw new ArgumentException("OriginalTitle must be between 5 and 255 characters long and contain only letters, spaces, hyphens, apostrophes, or commas.");
            }
        }

        public void ValidateStartYear()
        {
            if (StartYear < 1888 || StartYear > _year.Year)
            {
                throw new ArgumentOutOfRangeException(nameof(RuntimeMinutes), "StartYear must be between 1888 and the current year.");
            }
        }

        public void ValidateEndYear()
        {
            if (EndYear < 1888 || EndYear > _year.Year)
            {
                throw new ArgumentOutOfRangeException(nameof(RuntimeMinutes), "EndYear must be between 1888 and the current year.");

            }
        }
        public void ValidateRuntimeMinutes()
        {
            if (RuntimeMinutes is < 60 or > 1440)
            {
                throw new ArgumentOutOfRangeException(nameof(RuntimeMinutes), "RuntimeMinutes must be between 1 and 500.");
            }
        }


        public void Validate()
        {
            ValidatePrimaryTitle();
            ValidateOriginalTitle();
            ValidateTitleType();
            ValidateStartYear();
            ValidateEndYear();
            ValidateRuntimeMinutes();

        }


    }
}
