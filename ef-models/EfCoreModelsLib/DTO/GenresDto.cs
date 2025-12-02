
using System.ComponentModel.DataAnnotations;

namespace EfCoreModelsLib.DTO
{
    public class GenresDto : IObjectId
    {
        public required string Genre { get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Genre) && string.IsNullOrEmpty(Genre))
            {
                throw new ValidationException("Genre cannot be null or empty.");
            }
            if (Genre.Length < 3)
            {
                throw new ValidationException("Genre has to be more then 3 characters.");
            }
            if (Genre.Length > 50)
            {
                throw new ValidationException("Genre cannot be more then 50 characters.");
            }
            if (Genre.Any(c => !char.IsLetter(c)))
            {
                throw new ValidationException("Genre can only contain letters.");
            }
        }
    }
}
