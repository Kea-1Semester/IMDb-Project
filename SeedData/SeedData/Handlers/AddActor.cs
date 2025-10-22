using EfCoreModelsLib.Models.Mysql;

namespace SeedData.Handlers
{
    public static class AddActor
    {
        public static async Task AddActorToDb(ImdbContext context, string path, Dictionary<string, Guid> titleIdsDict, Dictionary<string, Guid> personIdsDict)
        {
            Console.WriteLine("Seed data in AddActor");

            var actors = new List<Actor>();

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 65536, FileOptions.Asynchronous))
            {
                using (var reader = new StreamReader(stream))
                {
                    await reader.ReadLineAsync(); // Skip header line

                    string? line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        var columns = line.Split('\t');
                        var tconst = columns[0];
                        var nconst = columns[2];
                        // Defensive: check if parts[5] exists and is not null
                        var role = columns.Length > 5 && !string.IsNullOrWhiteSpace(columns[5]) ? columns[5].Trim('"') : null;
                        //Role allow not null
                        if (titleIdsDict.TryGetValue(tconst, out var titleId) && personIdsDict.TryGetValue(nconst, out var personId))
                        {
                            if (!string.IsNullOrWhiteSpace(role))
                            {
                                actors.Add(new Actor
                                {
                                    ActorId = Guid.NewGuid(),
                                    TitlesTitleId = titleId,
                                    PersonsPersonId = personId,
                                    Role = role
                                });
                            }
                        }
                    }
                }
            }

            await context.Actors.AddRangeAsync(actors);
            await context.SaveChangesAsync();

            Console.WriteLine($"Seeded first actor records.");
        }
    }
}
