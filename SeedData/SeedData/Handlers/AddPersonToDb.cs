using EfCoreModelsLib.Models.Mysql;
using Microsoft.EntityFrameworkCore;

namespace SeedData.Handlers
{
    public static class AddPersonToDb
    {
        public static async Task<Dictionary<string, Guid>> AddPerson(ImdbContext context, string personTsv, int noOfRow, Dictionary<string, Guid> titleIdsDict)
        {
            Console.WriteLine("Seed dat for AddPerson To Db");

            var personIdsDict = new Dictionary<string, Guid>();

            bool anyPersons = await context.Persons.AnyAsync();
            bool anyProfessions = await context.Professions.AnyAsync();

            if (!anyPersons && !anyProfessions)
            {
                var persons = new List<Persons>();
                var professions = new List<Professions>();
                var titleDict = context.Titles.ToDictionary(t => t.TitleId, t => t);

                var professionsDic = context.Professions.ToDictionary(p => p.Profession, p => p.ProfessionId);

                using (var stream = new FileStream(personTsv, FileMode.Open, FileAccess.Read, FileShare.Read, 65536, FileOptions.Asynchronous))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        await reader.ReadLineAsync(); // Skip header line

                        string? line;
                        int count = 0;
                        while ((line = await reader.ReadLineAsync()) != null)
                        {
                            var columns = line.Split('\t');

                            Guid personId = Guid.NewGuid();

                            var person = new Persons
                            {
                                PersonId = personId,
                                Name = columns[1],
                                BirthYear = int.TryParse(columns[2], out var start) ? start : DateTime.UtcNow.Year,
                                EndYear = int.TryParse(columns[3], out int result) ? result : null
                            };

                            // Use a HashSet to track professions already added in this batch (case-insensitive)
                            var addedProfessions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                            var professionsList = columns[4]
                                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                            foreach (var profession in professionsList)
                            {
                                // Skip if already in DB or already added in this batch
                                if (professionsDic.ContainsKey(profession) || !addedProfessions.Add(profession))
                                    continue;

                                var professionEntity = new Professions
                                {
                                    ProfessionId = Guid.NewGuid(),
                                    PersonId = personId,
                                    Profession = profession
                                };
                                professionsDic[profession] = professionEntity.ProfessionId;
                                professions.Add(professionEntity);
                            }

                            var knownForTitles = columns[5]
                                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                            foreach (var knownFor in knownForTitles)
                            {
                                if (titleIdsDict.TryGetValue(knownFor, out var titleId))
                                {
                                    if (titleDict.TryGetValue(titleId, out var title))
                                    {
                                        person.KnownFor.Add(new KnownFor
                                        {
                                            KnownForId = Guid.NewGuid(),
                                            PersonsPersonId = personId,
                                            TitlesTitleId = titleId
                                        });
                                    }
                                }
                            }

                            personIdsDict.Add(columns[0], personId);
                            persons.Add(person);

                            count++;
                            if (count >= noOfRow)
                            {
                                break;
                            }
                        }
                    }
                }

                try
                {
                    await context.Persons.AddRangeAsync(persons);
                    Console.WriteLine("Adding Persons");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}: {ex.InnerException?.Message}");
                }

                try
                {
                    await context.Professions.AddRangeAsync(professions);
                    Console.WriteLine("Adding Professions");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}: {ex.InnerException?.Message}");
                }

                try
                {
                    await context.SaveChangesAsync();
                    Console.WriteLine("Saving Persons and Professions");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}: {ex.InnerException?.Message}");
                }
            }
            else
            {
                Console.WriteLine("Database already have Persons and Professions");
            }

            return personIdsDict;
        }
    }
}