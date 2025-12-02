
namespace EfCoreModelsLib.DTO
{
    public class TitlesDto : IObjectId
    {
        public string TitleType { get; set; }
        public string PrimaryTitle { get; set; }
        public string OriginalTitle { get; set; }
        public bool IsAdult { get; set; }
        public int StartYear { get; set; }
        public int RuntimeMinutes { get; set; }

        public void ValidateTitleType()
        {
            if (string.IsNullOrWhiteSpace(TitleType) && string.IsNullOrEmpty(TitleType))
            {
                throw new ArgumentException("TitleType cannot be null or empty.");
            }
            if (TitleType.Length is < 5 or > 25 || !TitleType.All(char.IsLetter))
            {
                throw new ArgumentException("TitleType must be between 5 and 25 characters long and contain only letters.");
            }
        }

        public void ValidatePrimaryTitle()
        {
            if (string.IsNullOrWhiteSpace(PrimaryTitle) && string.IsNullOrEmpty(PrimaryTitle))
            {
                throw new ArgumentException("PrimaryTitle cannot be null or empty.");
            }
            if (PrimaryTitle.Length is < 5 or > 255 && !PrimaryTitle.All(c => char.IsLetter(c) || char.IsWhiteSpace(c) || c == '-' || c == '\'' || c == ','))
            {
                throw new ArgumentException("PrimaryTitle must be between 1 and 100 characters long.");
            }
        }
        public void ValidateOriginalTitle()
        {

        }

        public void ValidateIsAdult()
        {

        }

        public void ValidateStartYear()
        {

        }
        public void ValidateRuntimeMinutes()
        {

        }


        public void Validate()
        {
            ValidatePrimaryTitle();
            ValidateOriginalTitle();
            ValidateIsAdult();
        }


    }
}
