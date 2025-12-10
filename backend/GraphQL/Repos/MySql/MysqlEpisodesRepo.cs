using EfCoreModelsLib.Models.Mysql;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Repos.Mysql
{
    public interface IMysqlEpisodesRepo
    {
        IQueryable<Episodes> GetMysqlEpisodes();
        Task<Episodes> CreateMysqlEpisode(Episodes episode);
        Task<Episodes> UpdateMysqlEpisode(Episodes episode);
        Task<Episodes> DeleteMysqlEpisode(Episodes episode);
    }

    public class MysqlEpisodesRepo : IMysqlEpisodesRepo, IAsyncDisposable
    {
        private readonly ImdbContext _context;

        public MysqlEpisodesRepo(IDbContextFactory<ImdbContext> dbContextFactory)
        {
            _context = dbContextFactory.CreateDbContext();
        }


        /* ---------- QUERIES ---------- */
        public IQueryable<Episodes> GetMysqlEpisodes()
        {
            return _context.Episodes.AsQueryable();
        }

        
        /* ---------- MUTATIONS ---------- */
        public async Task<Episodes> CreateMysqlEpisode(Episodes episode)
        {
            _context.Episodes.Add(episode);
            await _context.SaveChangesAsync();
            return episode;
        }

        public async Task<Episodes> UpdateMysqlEpisode(Episodes episode)
        {
            _context.Episodes.Update(episode);
            await _context.SaveChangesAsync();
            return episode;
        }

        public async Task<Episodes> DeleteMysqlEpisode(Episodes episode)
        {
            _context.Episodes.Remove(episode);
            await _context.SaveChangesAsync();
            return episode;
        }

        public ValueTask DisposeAsync()
        {
            return _context.DisposeAsync();
        }
    }
}