using SeedData.Models;

namespace SeedData.Handlers
{
    public static class TitleBasicsHandler
    {
        public static void SeedTitleBasics(ImdbContext context, string titleBasicPath)
        {
            var titleBasics = new List<TitleBasic>();
            var genreSet = new HashSet<string>();

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

                var genres = columns[8].Split(',', '|');

                foreach (var genre in genres)
                {
                    if (genreSet.Add(genre))
                    {
                        /*var titleGenre = new TitleGenre { Genre = genre };
                        context.TitleGenres.Add(titleGenre);
                        context.SaveChanges();
                        titleBasic.IdGenres.Add(titleGenre);*/
                        SeedGenres(genre, context, titleBasic);
                    }
                    else
                    {
                        var existingGenre = context.TitleGenres.First(g => g.Genre == genre);
                        titleBasic.IdGenres.Add(existingGenre);
                    }
                }

                titleBasics.Add(titleBasic);

            }

            context.TitleBasics.AddRange(titleBasics);
            context.SaveChanges();

            Console.WriteLine("Seeded first 50,000 TitleBasic records.");
        }

        private static void SeedGenres(string genre, ImdbContext context, TitleBasic titleBasic)
        {
            var titleGenre = new TitleGenre { Genre = genre };
            context.TitleGenres.Add(titleGenre);
            context.SaveChanges();
            titleBasic.IdGenres.Add(titleGenre);
        }
    }
}

