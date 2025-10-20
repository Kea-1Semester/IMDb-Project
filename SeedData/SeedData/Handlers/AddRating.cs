using SeedData.Models;

namespace SeedData.Handlers
{
    public class AddRating
    {
        public static void AddRatingToDb(ImdbContext context, string path)
        {
            Console.WriteLine("Seeding data in addRating");
            // get existing titles from the database
            var titles = context.Titles.ToDictionary(t => t.TitleId, t => t);
            var ratings = new List<Rating>();
            var ratPath = File.ReadAllLines(path).Skip(1);
            var rats = ratPath
                .Select(line => line.Split('\t'))
                .Select(parts => new Rating
                {
                    RatingId = Guid.NewGuid(),
                    TitleId = parts[0],
                    AverageRating = double.TryParse(parts[1], out var avgRating) ? avgRating : 0.0,
                    NumVotes = int.TryParse(parts[2], out var numVotes) ? numVotes : 0
                })
                .Where(rating => titles.ContainsKey(rating.TitleId))
                .ToList();

            context.Ratings.AddRange(rats);
            context.SaveChanges();
            Console.WriteLine("Seeded Rating records.");



        }
    }
}
