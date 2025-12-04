using System.ComponentModel.DataAnnotations;

namespace EfCoreModelsLib.DTO
{
    public class PersonsDto : IObjectId
    {
        public required string Name { get; set; }
        public int BirthYear { get; set; }
        public int? EndYear { get; set; }

        public void ValidateName()
        {
            if (string.IsNullOrWhiteSpace(Name) && string.IsNullOrEmpty(Name))
            {
                throw new ValidationException("Name is required");
            }
            if (Name.Length < 2)
            {
                throw new ValidationException("Name must be at least 2 characters long");
            }
            if (Name.Length > 100)
            {
                throw new ValidationException("Name cannot be longer than 100 characters");
            }

            var invalidChars = Name
                .Where(c => !char.IsLetter(c) && !char.IsWhiteSpace(c))
                .Distinct()
                .ToList();

            if (invalidChars.Any())
            {
                throw new ValidationException($"Name contains invalid characters: {string.Join(", ", invalidChars)}");
            }
        }

        public void ValidateBirthYear()
        {
            if (BirthYear < 1888)
            {
                throw new ValidationException("Birth year cannot be earlier than 1888");
            }
            if (BirthYear > DateTime.Now.Year)
            {
                throw new ValidationException("Birth year cannot be in the future");
            }
        }

        public void ValidateEndYear()
        {
            if (EndYear.HasValue)
            {
                if (EndYear < 1888)
                {
                    throw new ValidationException("End year cannot be earlier than 1888");
                }
                if (EndYear < BirthYear)
                {
                    throw new ValidationException("End year cannot be earlier than birth year");
                }
                if (EndYear > DateTime.Now.Year)
                {
                    throw new ValidationException("End year cannot be in the future");
                }
            }
        }

        public void Validate()
        {
            ValidateName();
            ValidateBirthYear();
            ValidateEndYear();
        }
    }
}
