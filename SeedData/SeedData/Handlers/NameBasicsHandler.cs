//using SeedData.Models;

//namespace SeedData.Handlers
//{
//    public static class NameBasicsHandler
//    {
//        public static void SeedNameBasics(ImdbContext context, string nameBasicPath)
//        {
//            var nameBasics = new List<NameBasic>();

//            // Seed NameBasic
//            foreach (var line in File.ReadLines(nameBasicPath).Skip(1).Take(50000))
//            {
//                var columns = line.Split('\t');
//                var nameBasic = new NameBasic
//                {
//                    Nconst = columns[0],
//                    PrimaryName = columns[1],
//                    BirthYear = int.TryParse(columns[2], out var birthYear) ? birthYear : 0,
//                    EndYear = int.TryParse(columns[3], out var deathYear) ? deathYear : null
//                };
//                nameBasics.Add(nameBasic);
//            }

//            context.NameBasics.AddRange(nameBasics);
//            context.SaveChanges();

//            Console.WriteLine("Seeded first 50,000 NameBasic records.");
//        }
//    }
//}