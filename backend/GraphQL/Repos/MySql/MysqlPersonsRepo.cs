using EfCoreModelsLib.Models.Mysql;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Repos.Mysql
{
    public interface IMysqlPersonsRepo
    {
        IQueryable<Persons> GetMySqlPersons();
        Task<Persons?> GetMySqlPerson(Guid id);
        Task<Persons> CreateMySqlPerson(Persons person);
        Task<Persons> UpdateMySqlPerson(Persons person);

        Task<Persons> DeleteMySqlPerson(Persons person);
    }

    public class MysqlPersonsRepo : IMysqlPersonsRepo, IAsyncDisposable
    {
        private readonly ImdbContext _context;

        public MysqlPersonsRepo(IDbContextFactory<ImdbContext> dbContextFactory)
        {
            _context = dbContextFactory.CreateDbContext();
        }

        public IQueryable<Persons> GetMySqlPersons()
        {
            return _context.Persons.AsQueryable();
        }

        public async Task<Persons?> GetMySqlPerson(Guid id)
        {
            return await _context.Persons.FirstOrDefaultAsync(p => p.PersonId == id);
        }

        public async Task<Persons> CreateMySqlPerson(Persons person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task<Persons> UpdateMySqlPerson(Persons person)
        {
            _context.Persons.Update(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task<Persons> DeleteMySqlPerson(Persons person)
        {
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public ValueTask DisposeAsync()
        {
            return _context.DisposeAsync();
        }
    }
}