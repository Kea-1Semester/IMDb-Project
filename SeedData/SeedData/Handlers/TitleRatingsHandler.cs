using SeedData.Models;

namespace SeedData.Handlers
{
    public static class TitleRatingsHandler
    {
        public static void SeedTitleRatings(ImdbContext context, string titleRatingsPath)
        {
            try
            {
                var titleRatings = new List<TitleRating>();

                // Seed titleRating
                foreach (var line in File.ReadLines(titleRatingsPath).Skip(1).Take(50000))
                {
                    var columns = line.Split('\t');
                    var existing = context.TitleBasics.FirstOrDefault(basic => basic.Tconst == columns[0]);
                    if (existing is not null)
                    {
                        var titleRating = new TitleRating
                        {
                            AverageRating = double.TryParse(columns[1], out var rating) ? rating : 0,
                            NumVotes = int.TryParse(columns[2], out var vote) ? vote : 0,
                            TconstNavigation = existing
                        };
                        titleRatings.Add(titleRating);
                    }
                }

                context.TitleRatings.AddRange(titleRatings);
                context.SaveChanges();
                var count = context.TitleRatings.Count();

                Console.WriteLine($"Seeded first {count} TitleRating records.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
            }
        }
    }
}