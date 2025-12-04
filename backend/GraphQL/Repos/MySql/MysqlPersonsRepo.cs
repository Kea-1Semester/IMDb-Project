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

        Task<Persons> DeleteMySqlPerson(Persons persons);
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

        public async Task<Persons> CreateMySqlPerson(Persons persons)
        {
            _context.Persons.Add(persons);
            await _context.SaveChangesAsync();
            return persons;
        }

        public async Task<Persons> UpdateMySqlPerson(Persons persons)
        {
            _context.Persons.Update(persons);
            await _context.SaveChangesAsync();
            return persons;
        }

        public async Task<Persons> DeleteMySqlPerson(Persons persons)
        {
            _context.Persons.Remove(persons);
            await _context.SaveChangesAsync();
            return persons;
        }

        public ValueTask DisposeAsync()
        {
            return _context.DisposeAsync();
        }
    }
}