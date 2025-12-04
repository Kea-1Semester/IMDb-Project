
namespace EfCoreModelsLib.DTO
{
    public class TitlesDto : IObjectId
    {
        private const int BUSINESS_MAX_FUTURE_YEARS = 50;
        private int currentYear = DateTime.UtcNow.Year;


        public required string TitleType { get; set; }
        public required string PrimaryTitle { get; set; }
        public required string OriginalTitle { get; set; }
        public bool IsAdult { get; set; }
        public int StartYear { get; set; }
        public int? EndYear { get; set; }
        public int? RuntimeMinutes { get; set; }

        public void ValidateTitleType()
        {
            if (string.IsNullOrWhiteSpace(TitleType) && string.IsNullOrEmpty(TitleType))
            {
                throw new InvalidOperationException("TitleType cannot be null or empty.");
            }

            if (TitleType.Length is < 5 or > 25)
            {
                throw new ArgumentException(
                    "TitleType must be between 5 and 25 characters long.", TitleType.ToString());
            }

            if (!TitleType.All(char.IsLetter))
            {
                throw new ArgumentException(
                    "TitleType can only contain only letters.", TitleType.ToString());
            }
        }

        public void ValidatePrimaryTitle()
        {
            if (string.IsNullOrWhiteSpace(PrimaryTitle) && string.IsNullOrEmpty(PrimaryTitle))
            {
                throw new InvalidOperationException("PrimaryTitle cannot be null or empty.");
            }

            if (PrimaryTitle.Length < 5 || PrimaryTitle.Length > 255)
            {
                throw new ArgumentException(
                    "PrimaryTitle must be between 5 and 255 characters.", PrimaryTitle.ToString());
            }
            if (!PrimaryTitle.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) ||
                                       c == '-' || c == '\'' || c == ',' || c == '.' || c == ':'))
            {
                throw new ArgumentException(
                    "PrimaryTitle can only contain only letters, digits, spaces, hyphens, apostrophes, or commas.", PrimaryTitle.ToString());
            }
        }

        public void ValidateOriginalTitle()
        {
            if (string.IsNullOrWhiteSpace(OriginalTitle) && string.IsNullOrEmpty(OriginalTitle))
            {
                throw new InvalidOperationException("OriginalTitle cannot be null or empty.");
            }

            if (OriginalTitle.Length < 5 || OriginalTitle.Length > 255)
            {
                throw new ArgumentException(
                    "OriginalTitle must be between 5 and 255 characters long.", OriginalTitle.ToString());
            }

            if (!OriginalTitle.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) ||
                                        c == '-' || c == '\'' || c == ',' || c == '.' || c == ':'))
            {
                throw new ArgumentException(
                    "OriginalTitle can only contain only letters, spaces, hyphens, apostrophes, or commas.", OriginalTitle.ToString());
            }
        }

        public void ValidateStartYear()
        {
            if (StartYear < 1888 || StartYear > currentYear)
            {
                throw new ArgumentException(
                    "StartYear must be between 1888 and the current year.", StartYear.ToString());
            }
        }

        public void ValidateEndYear()
        {
            int businessLimitYear = currentYear + BUSINESS_MAX_FUTURE_YEARS;

            if (EndYear < StartYear)
            {
                throw new ArgumentException("EndYear cannot be in the past.", EndYear.ToString());
            }

            if (EndYear > businessLimitYear)
            {
                throw new ArgumentException(
                    $"EndYear cannot be more than {BUSINESS_MAX_FUTURE_YEARS} years in the future.", EndYear.ToString());
            }
        }

        public void ValidateRuntimeMinutes()
        {
            if (RuntimeMinutes is < 1 or > 500)
            {
                throw new ArgumentException(
                    "RuntimeMinutes must be between 1 and 500.", RuntimeMinutes.ToString());
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
