using EfCoreModelsLib.DTO;
using EfCoreModelsLib.Models.Mysql;
using Microsoft.EntityFrameworkCore;
using GraphQL.Repos.Mysql;

namespace GraphQL.Services.Mysql
{
    public interface IMysqlPersonsService
    {
        IQueryable<Persons> GetMysqlPersons();
        Task<Persons?> GetMysqlPerson(Guid id);
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

        public async Task<Persons?> GetMysqlPerson(Guid id)
        {
            return await _personsRepo.GetMySqlPerson(id);
        }

        public async Task<Persons> CreateMysqlPerson(PersonsDto personsDto)
        {
            try
            {
                personsDto.Validate();
                Persons persons = new Persons
                {
                    PersonId = Guid.NewGuid(),
                    Name = personsDto.Name,
                    BirthYear = personsDto.BirthYear,
                    EndYear = personsDto.EndYear
                };
                return await _personsRepo.CreateMySqlPerson(persons);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Persons> UpdateMysqlPerson(PersonsDto person, Guid id)
        {
            try 
            {
                person.Validate();
                Persons? personToUpdate = await _personsRepo.GetMySqlPerson(id);
                if (personToUpdate == null)
                {
                    throw new Exception("Person not found");
                }
                personToUpdate.Name = person.Name;
                personToUpdate.BirthYear = person.BirthYear;
                personToUpdate.EndYear = person.EndYear;

                return await _personsRepo.UpdateMySqlPerson(personToUpdate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Persons> DeleteMysqlPerson(Guid PersonId)
        {
            try
            {
                Persons? personToDelete = await _personsRepo.GetMySqlPerson(PersonId);
                if (personToDelete == null)
                {
                    throw new Exception("Person not found");
                }
                return await _personsRepo.DeleteMySqlPerson(personToDelete);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}