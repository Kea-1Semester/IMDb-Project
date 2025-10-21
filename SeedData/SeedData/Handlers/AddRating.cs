using SeedData.Models;

namespace SeedData.Handlers
{
    public class AddRating
    {
        public static async Task AddRatingToDb(ImdbContext context, string path, Dictionary<string, Guid> titleIdsDict)
        {
            Console.WriteLine("Seeding data in addRating");
            // get existing titles from the database

            var ratings = new List<Rating>();

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
                            ratings.Add(new Rating
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

            await context.Ratings.AddRangeAsync(ratings);
            await context.SaveChangesAsync();

            Console.WriteLine("Seeded Rating records.");
        }
    }
}
