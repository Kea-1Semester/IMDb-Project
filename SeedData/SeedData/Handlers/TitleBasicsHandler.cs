using SeedData.Models;

namespace SeedData.Handlers
{
    public static class TitleBasicsHandler
    {
        public static void SeedTitleBasics(ImdbContext context, string titleBasicPath)
        {
            var titleBasics = new List<TitleBasic>();

            // Seed TitleBasic
            foreach (var line in File.ReadLines(titleBasicPath).Skip(1).Take(50000))
            {
                var columns = line.Split('\t');
                var titleBasic = new TitleBasic
                {
                    Tconst = columns[0],
                    TitleType = columns[1],
                    PrimaryTitle = columns[2],
                    OriginalTitle = columns[3],
                    IsAdult = sbyte.TryParse(columns[4], out var isAdult) ? isAdult : (sbyte)0,
                    StartYear = int.TryParse(columns[5], out var startYear) ? startYear : default,
                    EndYear = int.TryParse(columns[6], out var endYear) ? endYear : null,
                    RuntimeMinutes = int.TryParse(columns[7], out var runtime) ? runtime : null
                };
                titleBasics.Add(titleBasic);
            }

            context.TitleBasics.AddRange(titleBasics);
            context.SaveChanges();

            Console.WriteLine("Seeded first 50,000 TitleBasic records.");
        }
    }
}