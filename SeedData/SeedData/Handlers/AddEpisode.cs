using SeedData.Models;

namespace SeedData.Handlers
{
    public static class AddEpisode
    {
        public static void AddEpisodes(ImdbContext context, string path, int noOfRow)
        {
            Console.WriteLine("Seed data for AddEpisode");
            var titlesDict = context.Titles.ToDictionary(t => t.TitleId, t => t);
            var episodes = File.ReadAllLines(path)
                .Skip(1)
                .Select(line => line.Split('\t'))
                .Select(parts => new Episode
                {
                    EpisodeId = Guid.NewGuid(),
                    TitleIdParent = parts[0],
                    TitleIdChild = parts[1],
                    SeasonNumber = Parse(parts[2]),
                    EpisodeNumber = Parse(parts[3])
                })
                .Where(episode => titlesDict.ContainsKey(episode.TitleIdParent) && titlesDict.ContainsKey(episode.TitleIdChild))
                .ToList();
            context.Episodes.AddRange(episodes);
            context.SaveChanges();
            Console.WriteLine($"Seeded first {noOfRow} Episode records.");
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
