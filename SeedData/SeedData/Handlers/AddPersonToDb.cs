using SeedData.Models;

namespace SeedData.Handlers
{
    public static class AddPersonToDb
    {
        public static void AddPerson(ImdbContext context, string personTsv)
        {
            // add person to db
            // add primary profession to db 
            // MySQL support rang year 1901 to 2155 for type YEAR if we want to use for old consider using int.
            var person = new List<Person>();
            var professions = new List<Profession>();
            var professionsDic = context.Professions.ToDictionary(p => p.Profession1, p => p.ProfessionId);
            // seed data
            foreach (var line in File.ReadAllLines(personTsv).Skip(1).Take(10))
            {
                var columns = line.Split('\t');
                var personId = Guid.NewGuid();
                var personEntity = new Person
                {
                    PersonId = personId,
                    Name = columns[1],
                    BirthYear = ParseYear(columns[2]),
                    EndYear = ParseYear(columns[3]) == 0 ? null : ParseYear(columns[3])
                };
                Console.WriteLine();

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
                        PersonId = personId,
                        Profession1 = profession
                    };
                    professionsDic[profession] = professionEntity.ProfessionId;
                    professions.Add(professionEntity);
                }
            }

            context.Persons.AddRange(person);
            context.Professions.AddRange( professions);


            context.SaveChanges();
            Console.WriteLine("Seeded first 10 Person records with Professions.");

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
