using EfCoreModelsLib.Models.Mysql;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SeedData.Handlers
{
    public static class TitleBasicsHandler
    {
        public static async Task<Dictionary<string, Guid>> SeedTitleBasics(ImdbContext context, string titleBasicPath, int noOfRow)
        {
            Console.WriteLine("Seed data for title");

            var titleIdsDict = new Dictionary<string, Guid>();
            bool anyTitles = await context.Titles.AnyAsync();
            bool anyGenres = await context.Genres.AnyAsync();

            if (!anyTitles && !anyGenres)
            {
                var titleBasics = new List<Titles>();
                var genreDict = context.Genres.ToDictionary(g => g.Genre, g => g); // Cache existing genres

                using (var stream = new FileStream(titleBasicPath, FileMode.Open, FileAccess.Read, FileShare.Read, 65536, FileOptions.Asynchronous))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        await reader.ReadLineAsync(); // Skip header line

                        // Seed TitleBasic
                        string? line;
                        int count = 0;
                        while ((line = await reader.ReadLineAsync()) != null)
                        {
                            var columns = line.Split('\t');

                            Guid newGuid = Guid.NewGuid();

                            var title = new Titles
                            {
                                TitleId = newGuid,
                                TitleType = columns[1],
                                PrimaryTitle = columns[2],
                                OriginalTitle = columns[3],
                                IsAdult = (columns[4] != "0"),
                                StartYear = int.TryParse(columns[5], out var start) ? start : DateTime.UtcNow.Year,
                                EndYear = int.TryParse(columns[6], out int result) ? result : null,
                                RuntimeMinutes = int.TryParse(columns[7], out var runtime) ? runtime : null
                            };


                            var genres = columns[8].Split(',', '|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                            foreach (var genre in genres)
                            {
                                if (string.IsNullOrWhiteSpace(genre))
                                    continue;

                                if (!genreDict.TryGetValue(genre, out var genreEntity))
                                {
                                    genreEntity = new Genres { GenreId = Guid.NewGuid(), Genre = genre };
                                    context.Genres.Add(genreEntity);
                                    genreDict[genre] = genreEntity;
                                }
                                title.GenresGenre.Add(genreEntity);
                            }

                            titleIdsDict.Add(columns[0], newGuid);
                            titleBasics.Add(title);

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
                    await context.Titles.AddRangeAsync(titleBasics);
                    Console.WriteLine("Adding Titles and Genres");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}: {ex.InnerException?.Message}");
                }

                try
                {
                    await context.SaveChangesAsync();
                    Console.WriteLine("Saving Titles and Genres");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}: {ex.InnerException?.Message}");
                }
            }
            else
            {
                Console.WriteLine("Database already have Titles and Genres");
            }

            return titleIdsDict;
        }
    }
}

