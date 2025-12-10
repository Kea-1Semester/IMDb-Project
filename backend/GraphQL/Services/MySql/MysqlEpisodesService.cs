using EfCoreModelsLib.DTO;
using EfCoreModelsLib.Models.Mysql;
using Microsoft.EntityFrameworkCore;
using GraphQL.Repos.Mysql;

namespace GraphQL.Services.Mysql
{
    public interface IMysqlEpisodesService
    {
        Task<Episodes> CreateMysqlEpisodes(EpisodesDto episode);
        Task<Episodes> UpdateMysqlEpisodes(EpisodesDto episode, Guid episodeId);
        Task<Episodes> DeleteMysqlEpisodes(Guid episodeId);
    }

    public class MysqlEpisodesService : IMysqlEpisodesService
    {
        private readonly IMysqlEpisodesRepo _episodesRepo;

        public MysqlEpisodesService(IMysqlEpisodesRepo episodesRepo)
        {
            _episodesRepo = episodesRepo;
        }

        public async Task<Episodes> CreateMysqlEpisodes(EpisodesDto episode)
        {
            episode.Validate();
            Episodes newEpisode = new Episodes
            {
                EpisodeId = Guid.NewGuid(),
                TitleIdParent = episode.ParentId,
                TitleIdChild = episode.ChildId,
                EpisodeNumber = episode.EpisodeNumber,
                SeasonNumber = episode.SeasonNumber,
            };
            return await _episodesRepo.CreateMysqlEpisode(newEpisode);
        }

        public async Task<Episodes> UpdateMysqlEpisodes(EpisodesDto  episode, Guid episodeId)
        {
            episode.Validate();
            Episodes? episodeToUpdate = await _episodesRepo.GetMysqlEpisodes().FirstOrDefaultAsync(e => e.EpisodeId == episodeId);
            if (episodeToUpdate == null)
            {
                throw new GraphQLException(new Error("Episode not found","EPISODE_NOT_FOUNT"));
            }
            episodeToUpdate.TitleIdParent = episode.ParentId;
            episodeToUpdate.TitleIdChild = episode.ChildId;
            episodeToUpdate.EpisodeNumber = episode.EpisodeNumber;
            episodeToUpdate.SeasonNumber = episode.SeasonNumber;

            return await _episodesRepo.UpdateMysqlEpisode(episodeToUpdate);
        }

        public async Task<Episodes> DeleteMysqlEpisodes(Guid episodeId)
        {
            Episodes? episodeToDelete = await _episodesRepo.GetMysqlEpisodes().FirstOrDefaultAsync(e => e.EpisodeId == episodeId);
            if (episodeToDelete == null)
            {
                throw new GraphQLException(new Error("Episode not found", "EPISODE_NOT_FOUND"));
            }
            return await _episodesRepo.DeleteMysqlEpisode(episodeToDelete);
        }
    }
}