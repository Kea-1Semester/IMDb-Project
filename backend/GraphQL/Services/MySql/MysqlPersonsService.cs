using EfCoreModelsLib.DTO;
using EfCoreModelsLib.Models.Mysql;
using Microsoft.EntityFrameworkCore;
using GraphQL.Repos.Mysql;

namespace GraphQL.Services.Mysql
{
    public interface IMysqlPersonsService
    {
        IQueryable<Persons> GetMysqlPersons();
        Task<Persons> CreateMysqlPerson(PersonsDto person);
        Task<Persons> UpdateMysqlPerson(PersonsDto person, Guid id);
        Task<Persons> DeleteMysqlPerson(Guid PersonId);
    }

    public class MysqlPersonsService : IMysqlPersonsService
    {
        private readonly IMysqlPersonsRepo _personsRepo;

        public MysqlPersonsService(IMysqlPersonsRepo personsRepo)
        {
            _personsRepo = personsRepo;
        }

        public IQueryable<Persons> GetMysqlPersons()
        {
            return _personsRepo.GetMySqlPersons();
        }

        public async Task<Persons> CreateMysqlPerson(PersonsDto person)
        {
            person.Validate();
            Persons persons = new Persons
            {
                PersonId = Guid.NewGuid(),
                Name = person.Name,
                BirthYear = person.BirthYear,
                EndYear = person.EndYear
            };
            return await _personsRepo.CreateMySqlPerson(persons);
        }

        public async Task<Persons> UpdateMysqlPerson(PersonsDto person, Guid id)
        {
            person.Validate();
            Persons? personToUpdate = await _personsRepo.GetMySqlPersons().FirstOrDefaultAsync(p => p.PersonId == id);
            if (personToUpdate == null)
            {
                throw new GraphQLException(new Error("Person not found", "PERSON_NOT_FOUND"));
            }
            personToUpdate.Name = person.Name;
            personToUpdate.BirthYear = person.BirthYear;
            personToUpdate.EndYear = person.EndYear;

            return await _personsRepo.UpdateMySqlPerson(personToUpdate);
        }

        public async Task<Persons> DeleteMysqlPerson(Guid PersonId)
        {
            Persons? personToDelete = await _personsRepo.GetMySqlPersons().FirstOrDefaultAsync(p => p.PersonId == PersonId);
            if (personToDelete == null)
            {
                throw new GraphQLException(new Error("Person not found", "PERSON_NOT_FOUND"));
            }
            return await _personsRepo.DeleteMySqlPerson(personToDelete);
        }
    }
}