
namespace EfCoreModelsLib.DTO
{
    public class TitlesDto : IObjectId
    {
        private const int BUSINESS_MAX_FUTURE_YEARS = 50;


        public required string TitleType { get; set; }
        public required string PrimaryTitle { get; set; }
        public required string OriginalTitle { get; set; }
        public bool IsAdult { get; set; }
        private readonly DateOnly _year = DateOnly.FromDateTime(DateTime.Now);

        private int _startYear;

        public int StartYear
        {
            get => _startYear;
            set =>
                // Use Math.Abs to ensure only non-negative values are set
                _startYear = Math.Abs(value);
        }

        private int? _endYear;

        public int? EndYear
        {
            get => _endYear;
            set =>
                _endYear = value.HasValue ? Math.Abs(value.Value) : (int?)null;
        }

        private int? _runtimeMinutes;

        public int? RuntimeMinutes
        {
            get => _runtimeMinutes;
            set =>
                _runtimeMinutes = value.HasValue ? Math.Abs(value.Value) : (int?)null;
        }


        public void ValidateTitleType()
        {
            if (string.IsNullOrWhiteSpace(TitleType) && string.IsNullOrEmpty(TitleType))
            {
                throw new InvalidOperationException("TitleType cannot be null or empty.");
            }

            if (TitleType.Length is < 5 or > 25 || !TitleType.All(char.IsLetter))
            {
                throw new ArgumentException(
                    "TitleType must be between 5 and 25 characters long and contain only letters.");
            }
        }

        public void ValidatePrimaryTitle()
        {
            if (string.IsNullOrWhiteSpace(PrimaryTitle) && string.IsNullOrEmpty(PrimaryTitle))
            {
                throw new InvalidOperationException("PrimaryTitle cannot be null or empty.");
            }

            if (PrimaryTitle.Length < 5 || PrimaryTitle.Length > 255 ||
                !PrimaryTitle.All(c => char.IsLetter(c) || char.IsWhiteSpace(c) ||
                                       c == '-' || c == '\'' || c == ',' || c == '.' || c == ':'))
            {
                throw new ArgumentException(
                    "PrimaryTitle must be between 5 and 255 characters long and contain only letters, spaces, hyphens, apostrophes, or commas.");
            }
        }

        public void ValidateOriginalTitle()
        {
            if (string.IsNullOrWhiteSpace(OriginalTitle) && string.IsNullOrEmpty(OriginalTitle))
            {
                throw new InvalidOperationException("OriginalTitle cannot be null or empty.");
            }

            if (OriginalTitle.Length < 5 || OriginalTitle.Length > 255 ||
                !OriginalTitle.All(c => char.IsLetter(c) || char.IsWhiteSpace(c) ||
                                        c == '-' || c == '\'' || c == ',' || c == '.' || c == ':'))
            {
                throw new ArgumentException(
                    "OriginalTitle must be between 5 and 255 characters long and contain only letters, spaces, hyphens, apostrophes, or commas.");
            }
        }

        public void ValidateStartYear()
        {
            if (StartYear < 1888 || StartYear > _year.Year)
            {
                throw new ArgumentException(
                    "StartYear must be between 1888 and the current year.");
            }
        }

        public void ValidateEndYear()
        {
            int currentYear = DateTime.Now.Year;
            int businessLimitYear = currentYear + BUSINESS_MAX_FUTURE_YEARS;

            if (EndYear < _year.Year)
            {
                throw new ArgumentException("EndYear cannot be in the past.");
            }

            if (EndYear > businessLimitYear)
            {
                throw new ArgumentException(
                    $"EndYear cannot be more than {BUSINESS_MAX_FUTURE_YEARS} years in the future.");
            }
        }

        public void ValidateRuntimeMinutes()
        {
            if (RuntimeMinutes is < 60 or > 1440)
            {
                throw new ArgumentException(
                    "RuntimeMinutes must be between 1 and 500.");
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
