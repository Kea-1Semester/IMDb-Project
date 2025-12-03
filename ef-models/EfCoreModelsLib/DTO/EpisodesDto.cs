using System.ComponentModel.DataAnnotations;

namespace EfCoreModelsLib.DTO
{
    public class EpisodesDto : IObjectId
    {
        public required Guid ParentId { get; set; }
        public required Guid ChildId { get; set; }
        public required int EpisodeNumber { get; set; }
        public required int SeasonNumber { get; set; }

        public void ValidateEpisodeNumber()
        {
            if (EpisodeNumber < 1)
            {
                throw new ValidationException("Episode number must be greater than 0.");
            }
            if (EpisodeNumber > 999)
            {
                throw new ValidationException("Episode number must be less than or equal to 999.");
            }
        }

        public void ValidateSeasonNumber()
        {
            if (SeasonNumber < 1)
            {
                throw new ValidationException("Season number must be greater than 0.");
            }
            if (SeasonNumber > 99)
            {
                throw new ValidationException("Season number must be less than or equal to 99.");
            }
        }

        public void Validate()
        {
            ValidateEpisodeNumber();
            ValidateSeasonNumber();
        }
    }
}
