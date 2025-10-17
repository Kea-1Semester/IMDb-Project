using SeedData.Models;

namespace SeedData.Handlers
{
    public static class AddPersonToDb
    {
        public static void AddPerson(ImdbContext context, string personTsv, int noOfRow, string principals)
        {
            Console.WriteLine("Seed dat for AddPerson To Db");
            // add person to db
            // add primary profession to db 
            // MySQL support rang year 1901 to 2155 for type YEAR if we want to use for old consider using int.
            var person = new List<Person>();
            var professions = new List<Profession>();
            var professionsDic = context.Professions.ToDictionary(p => p.Profession1, p => p.ProfessionId);
            var knownDict = context.Titles.ToDictionary(t => t.TitleId, t => t);
            // seed data
            foreach (var line in File.ReadAllLines(personTsv).Skip(1).Take(noOfRow))
            {
                var columns = line.Split('\t');
                //var personId = Guid.NewGuid();
                var personId = columns[0];
                var personEntity = new Person
                {
                    PersonId = personId,
                    Name = columns[1],
                    BirthYear = ParseYear(columns[2]),
                    EndYear = ParseYear(columns[3]) == 0 ? null : ParseYear(columns[3])
                };

                person.Add(personEntity);
                // Use a HashSet to track professions already added in this batch (case-insensitive)
                var addedProfessions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                var professionsList = columns[4]
                    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                foreach (var profession in professionsList)
                {
                    // Skip if already in DB or already added in this batch
                    if (professionsDic.ContainsKey(profession) || !addedProfessions.Add(profession))
                        continue;

                    var professionEntity = new Profession
                    {
                        ProfessionId = Guid.NewGuid(),
                        PersonId = columns[0],
                        Profession1 = profession
                    };
                    professionsDic[profession] = professionEntity.ProfessionId;
                    professions.Add(professionEntity);
                }


                //var addKnownForTitles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                var knownForTitles = columns[5]
                    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                foreach (var knownFor in knownForTitles)
                {
                    if (knownDict.TryGetValue(knownFor, out var title))
                    {
                        personEntity.TitlesTitlesNavigation.Add(title);
                    }
                }
                context.Persons.Add(personEntity);

                //// principals
                //var uniqueActors = new HashSet<(string TitleId, string PersonId)>();

                //foreach (var linePrincipal in File.ReadAllLines(principals).Skip(1))
                //{
                //    var parts = linePrincipal.Split('\t');
                //    var titleId = parts[0];
                //    var personIdPrincipal = parts[2];
                //    var role = parts.Length > 5 && !string.IsNullOrWhiteSpace(parts[5]) ? parts[5].Trim('"') : null;

                //    // Only add if this is the current person and the combination is unique
                //    if (personId == personIdPrincipal && uniqueActors.Add((titleId, personId)))
                //    {
                //        var title = context.Titles.FirstOrDefault(t => t.TitleId == titleId);
                //        if (title != null)
                //        {
                //            title.Actors.Add(new Actor
                //            {
                //                PersonsPersonId = personId,
                //                TitlesTitleId = titleId,
                //                Role = role
                //            });
                //        }
                //    }
                //}


            }

            context.Persons.AddRange(person);
            context.Professions.AddRange(professions);


            context.SaveChanges();
            Console.WriteLine($"Seeded first {noOfRow} Person records with Professions.");

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
public class KnownForTitle
{
    public string TitleId { get; set; }
    public string PersonId { get; set; }
}