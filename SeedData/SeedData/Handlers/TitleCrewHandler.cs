using SeedData.Models;

namespace SeedData.Handlers
{
    public static class TitleCrewHandler
    {
        public static void SeedTitleCrew(ImdbContext context, string titleCrewPath)
        {
            var titleCrews = new List<TitleCrew>();
            int lineCount = 0;

            // Seed TitleCrew
            foreach (var line in File.ReadLines(titleCrewPath).Skip(1).Take(50000))
            {
                lineCount++;
                var columns = line.Split('\t');
                if (columns.Length < 2) continue;

                var tconst = columns[0];

                var directors = columns[1] != @"\N" && !string.IsNullOrWhiteSpace(columns[1])
                    ? columns[1].Split(',')
                    : Array.Empty<string>();

                var writers = (columns.Length > 2 && columns[2] != @"\N" && !string.IsNullOrWhiteSpace(columns[2]))
                    ? columns[2].Split(',')
                    : Array.Empty<string>();

                if (!directors.Any() && !writers.Any())
                    continue;

                foreach (var nconst in directors.Concat(writers))
                {
                    titleCrews.Add(new TitleCrew
                    {
                        Tconst = tconst,
                        Nconst = nconst
                    });
                };
            }

            if (lineCount % 5000 == 0)
            {
                context.TitleCrews.AddRange(titleCrews);
                context.SaveChanges();

                titleCrews.Clear();
                Console.WriteLine("Seeded first 50,000 TitleCrew records.");
            };
        }
    }
}