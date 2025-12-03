using System.ComponentModel.DataAnnotations;

namespace EfCoreModelsLib.DTO
{
    public class AliasesDto : IObjectId
    {
        public required Guid TitleId { get; set; }
        public required string Region { get; set; }
        public required string Language { get; set; }
        public bool IsOriginalTitle { get; set; }
        public required string Title { get; set; }

        public void ValidateRegion()
        {
            if (string.IsNullOrEmpty(Region) && string.IsNullOrWhiteSpace(Region))
            {
                throw new ValidationException("Region cannot be null or empty.");
            }
            if (Region.Length < 2)
            {
                throw new ValidationException("Region must be at least 2 characters long.");
            }
            if (Region.Length > 5)
            {
                throw new ValidationException("Region cannot exceed 5 characters.");
            }

            var invalidChars = Region
                .Where(c => !char.IsLetter(c))
                .Distinct()
                .ToList();

            if (invalidChars.Any())
            {
                throw new ValidationException($"Region contains invalid characters: {string.Join(", ", invalidChars)}");
            }
        }

        public void ValidateLanguage()
        {
            if (string.IsNullOrEmpty(Language) && string.IsNullOrWhiteSpace(Language))
            {
                throw new ValidationException("Language cannot be null or empty.");
            }
            if (Language.Length < 2)
            {
                throw new ValidationException("Language must be at least 2 characters long.");
            }
            if (Language.Length > 5)
            {
                throw new ValidationException("Language cannot exceed 5 characters.");
            }

            var invalidChars = Language
                .Where(c => !(char.IsLetter(c) || c.Equals('-')))
                .Distinct()
                .ToList();

            if (invalidChars.Any())
            {
                throw new ValidationException($"Language contains invalid characters: {string.Join(", ", invalidChars)}");
            }
        }

        public void ValidateTitle()
        {
            if (string.IsNullOrEmpty(Title) && string.IsNullOrWhiteSpace(Title))
            {
                throw new ValidationException("Title cannot be null or empty.");
            }
            if (Title.Length < 5)
            {
                throw new ValidationException("Title must be at least 5 characters long.");
            }
            if (Title.Length > 255)
            {
                throw new ValidationException("Title cannot exceed 255 characters.");
            }

            var invalidChars = Title
                .Where(c => !(
                    char.IsLetterOrDigit(c) ||
                    char.IsWhiteSpace(c) ||
                    c.Equals('-') ||
                    c.Equals(',') ||
                    c.Equals('.') ||
                    c.Equals('!') ||
                    c.Equals('?') ||
                    c.Equals('’') ||
                    c.Equals(':') ||
                    c.Equals(';') ||
                    c.Equals('\'')))
                .Distinct()
                .ToList();

            if (invalidChars.Any())
            {
                throw new ValidationException($"Title contains invalid characters: {string.Join(", ", invalidChars)}");
            }
        }

        public void Validate()
        {
            ValidateRegion();
            ValidateLanguage();
            ValidateTitle();
        }
    }
}
