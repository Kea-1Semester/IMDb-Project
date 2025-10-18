using SeedData.Models;

namespace SeedData.Handlers
{
    public static class TitleBasicsHandler
    {
        public static void SeedTitleBasics(ImdbContext context, string titleBasicPath, int noOfRow)
        {
            Console.WriteLine("Seed data for title");
            // var person = new List<Person>();
            // var professions = new List<Profession>();


            var titleBasics = new List<Title>();
            var genreDict = context.Genres.ToDictionary(g => g.Genre1, g => g); // Cache existing genres

            // Seed TitleBasic
            foreach (var line in File.ReadLines(titleBasicPath).Skip(1).Take(noOfRow))
            {
                var columns = line.Split('\t');
                var title = new Title
                {
                    TitleId = columns[0],
                    TitleType = columns[1],
                    PrimaryTitle = columns[2],
                    OriginalTitle = columns[3],
                    IsAdult = sbyte.TryParse(columns[4], out var isAdult) ? isAdult == 1 : false,
                    StartYear = ParseYear(columns[5]),
                    EndYear = ParseYear(columns[6]),
                    RuntimeMinutes = int.TryParse(columns[7], out var runtime) ? runtime : null
                };

                var genres = columns[8].Split(',', '|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                foreach (var genre in genres)
                {
                    if (string.IsNullOrWhiteSpace(genre))
                        continue;

                    if (!genreDict.TryGetValue(genre, out var genreEntity))
                    {
                        genreEntity = new Genre { GenreId = Guid.NewGuid(), Genre1 = genre };
                        context.Genres.Add(genreEntity);
                        genreDict[genre] = genreEntity;
                    }
                    title.GenresGenres.Add(genreEntity);
                }

                titleBasics.Add(title);
            }

            context.Titles.AddRange(titleBasics);
            context.SaveChanges();

            Console.WriteLine($"Seeded first {noOfRow} TitleBasic records.");
        }

        static short ParseYear(string value)
        {
            if (value == "\\N") return 0;
            if (short.TryParse(value, out var year) && (year == 0 || (year >= 1901 && year <= 2155)))
                return year;
            // cant be null represented in db so return 9999
            return 0;
        }
    }
}

