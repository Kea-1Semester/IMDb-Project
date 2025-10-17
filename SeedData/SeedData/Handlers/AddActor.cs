using SeedData.Models;

namespace SeedData.Handlers
{
    public static class AddActor
    {
        public static void AddActorToDb(ImdbContext context, string path)
        {
            Console.WriteLine("Seed data in AddActor");
            var titlesDict = context.Titles.ToDictionary(t => t.TitleId, t => t);
            var personsDict = context.Persons.ToDictionary(p => p.PersonId, p => p);
            var id = 1;
            var actors = new List<Actor>();
            foreach (var line in File.ReadAllLines(path).Skip(1))
            {
                var parts = line.Split('\t');
                var titleId = parts[0];
                var personId = parts[2];
                // Defensive: check if parts[5] exists and is not null
                var role = parts.Length > 5 && !string.IsNullOrWhiteSpace(parts[5]) ? parts[5].Trim('"') : null;
                //Role allow not null
                if (titlesDict.ContainsKey(titleId) && personsDict.ContainsKey(personId))
                {
                    actors.Add(new Actor
                    {
                        Id = id++,
                        TitlesTitleId = titleId,
                        PersonsPersonId = personId,
                        Role = role ?? "Unknown"
                    });
                }
                //if (actors.Count >= noOfRow) break;
            }

            

            context.Actors.AddRange(actors);
            context.SaveChanges();
            Console.WriteLine($"Seeded first actor records.");
        }
    }
}
