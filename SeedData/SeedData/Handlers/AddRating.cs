using EfCoreModelsLib.Models.Mysql;
using Microsoft.EntityFrameworkCore;

namespace SeedData.Handlers
{
    public class AddRating
    {
        public static async Task AddRatingToDb(ImdbContext context, string path, Dictionary<string, Guid> titleIdsDict)
        {
            Console.WriteLine("Seeding data in addRating");
            // get existing titles from the database

            bool anyRatings = await context.Ratings.AnyAsync();

            if (!anyRatings)
            {
                var ratings = new List<Ratings>();

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
                            var titleId = titleIdsDict.ContainsKey(tconst) ? titleIdsDict[tconst] : Guid.Empty;

                            if (titleId != default)
                            {
                                ratings.Add(new Ratings
                                {
                                    RatingId = Guid.NewGuid(),
                                    TitleId = titleId,
                                    AverageRating = double.TryParse(columns[1], out var avgRating) ? avgRating : 0.0,
                                    NumVotes = int.TryParse(columns[2], out var numVotes) ? numVotes : 0
                                });
                            }
                        }
                    }
                }

                try
                {
                    await context.Ratings.AddRangeAsync(ratings);
                    Console.WriteLine("Adding Ratings");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}: {ex.InnerException?.Message}");
                }

                try
                {
                    await context.SaveChangesAsync();
                    Console.WriteLine("Saving Ratings");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}: {ex.InnerException?.Message}");
                }
            }
            else
            {
                Console.WriteLine("Database already have Ratings");
            }
        }
    }
}
