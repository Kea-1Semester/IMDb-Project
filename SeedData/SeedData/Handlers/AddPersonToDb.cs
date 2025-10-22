using EfCoreModelsLib.Models.Mysql;

namespace SeedData.Handlers
{
    public static class AddPersonToDb
    {
        public static async Task<Dictionary<string, Guid>> AddPerson(ImdbContext context, string personTsv, int noOfRow, Dictionary<string, Guid> titleIdsDict)
        {
            Console.WriteLine("Seed dat for AddPerson To Db");

            var personIdsDict = new Dictionary<string, Guid>();

            var persons = new List<Person>();
            var professions = new List<Profession>();
            var titleDict = context.Titles.ToDictionary(t => t.TitleId, t => t);

            var professionsDic = context.Professions.ToDictionary(p => p.Profession1, p => p.ProfessionId);

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

                        var person = new Person
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

                            var professionEntity = new Profession
                            {
                                ProfessionId = Guid.NewGuid(),
                                PersonId = personId,
                                Profession1 = profession
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
                                    person.TitlesTitlesNavigation.Add(title);
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

            await context.Persons.AddRangeAsync(persons);

            await context.SaveChangesAsync();

            await context.Professions.AddRangeAsync(professions);

            await context.SaveChangesAsync();

            Console.WriteLine($"Seeded first {noOfRow} Person records with Professions.");

            return personIdsDict;
        }
    }
}