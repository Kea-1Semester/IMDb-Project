using EfCoreModelsLib.Models.Mysql;
using Microsoft.EntityFrameworkCore;

namespace SeedData.Handlers
{
    public static class AddCrewToDb
    {
        public static async Task AddCrew(ImdbContext context, string path, int noOfRow, Dictionary<string, Guid> titleIdsDict, Dictionary<string, Guid> personIdsDict)
        {
            //Directors , writers 
            Console.WriteLine("Seed data in Crew");

            bool anyDirector = await context.Directors.AnyAsync();
            bool anyWriters = await context.Writers.AnyAsync();

            if (!anyDirector && !anyWriters)
            {
                var titlesDict = context.Titles.ToDictionary(t => t.TitleId, t => t);
                var personsDict = context.Persons.ToDictionary(p => p.PersonId, p => p);

                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 65536, FileOptions.Asynchronous))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        await reader.ReadLineAsync(); // Skip header line

                        string? line;
                        int count = 0;
                        while ((line = await reader.ReadLineAsync()) != null)
                        {
                            var columns = line.Split('\t');

                            var tconst = columns[0];

                            var title = titleIdsDict.TryGetValue(tconst, out var titleGuid) && titlesDict.TryGetValue(titleGuid, out var _title)
                                ? _title
                                : null;

                            var directors = columns[1]
                                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                            var writers = columns[2]
                                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                            foreach (var directorId in directors)
                            {
                                if (personIdsDict.TryGetValue(directorId, out var guid) && personsDict.TryGetValue(guid, out var person))
                                {
                                    title?.Directors.Add(new Directors
                                    {
                                        DirectorsId = Guid.NewGuid(),
                                        PersonsPersonId = person.PersonId,
                                        TitlesTitleId = title.TitleId
                                    });
                                }
                            }

                            foreach (var writerId in writers)
                            {
                                if (personIdsDict.TryGetValue(writerId, out var guid) && personsDict.TryGetValue(guid, out var person))
                                {
                                    title?.Writers.Add(new Writers
                                    {
                                        WritersId = Guid.NewGuid(),
                                        PersonsPersonId = person.PersonId,
                                        TitlesTitleId = title.TitleId
                                    });
                                }
                            }

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
                    await context.Directors.AddRangeAsync(context.Directors.Local);
                    await context.Writers.AddRangeAsync(context.Writers.Local);
                    await context.SaveChangesAsync();
                    Console.WriteLine("Saving Directors and Writers");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}: {ex.InnerException?.Message}");
                }
            }
            else
            {
                Console.WriteLine("Database already have Directors and Writers");
            }
        }
    }
}
