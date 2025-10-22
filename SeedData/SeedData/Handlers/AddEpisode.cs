using EfCoreModelsLib.Models.Mysql;
using Microsoft.EntityFrameworkCore;

namespace SeedData.Handlers
{
    public static class AddEpisode
    {
        public static async Task AddEpisodes(ImdbContext context, string path, int noOfRow, Dictionary<string, Guid> titleIdsDict)
        {
            Console.WriteLine("Seed data for AddEpisode");

            bool anyEpisodes = await context.Episodes.AnyAsync();

            if (!anyEpisodes)
            {
                var episodes = new List<Episodes>();

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
                            var titleIdParent = titleIdsDict.TryGetValue(columns[0], out var parent) ? parent : Guid.Empty;
                            var titleIdChild = titleIdsDict.TryGetValue(columns[1], out var child) ? child : Guid.Empty;

                            if (titleIdParent != default && titleIdChild != default)
                            {
                                episodes.Add(new Episodes
                                {
                                    EpisodeId = Guid.NewGuid(),
                                    TitleIdParent = titleIdParent,
                                    TitleIdChild = titleIdChild,
                                    SeasonNumber = Parse(columns[2]),
                                    EpisodeNumber = Parse(columns[3])
                                });
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
                    await context.Episodes.AddRangeAsync(episodes);
                    Console.WriteLine("Adding Episodes");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}: {ex.InnerException?.Message}");
                }

                try
                {
                    await context.SaveChangesAsync();
                    Console.WriteLine("Saving Episodes");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}: {ex.InnerException?.Message}");
                }
            }
            else
            {
                Console.WriteLine("Database already have Episodes");
            }
        }

        private static int Parse(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value == "\\N")
                return 0;

            if (int.TryParse(value, out var number) && number >= 0)
                return number;

            return 0;
        }
    }
}
