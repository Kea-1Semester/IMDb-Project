using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SeedData.Models;

namespace SeedData.Handlers
{
    public static class AddCrewToDb
    {
        public static void AddCrew(ImdbContext context, string path, int noOfRow)
        {

            //Directors , writers 
            Console.WriteLine("Seed data in AddCrew");

            foreach (var line in File.ReadAllLines(path).Skip(1).Take(noOfRow))
            {
                var columns = line.Split('\t');
                var titleId = columns[0];
                var directors = columns[1]
                    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                var writers = columns[2]
                    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                var title = context.Titles.Include(title => title.PersonsPeople).Include(title => title.PersonsPeople1).FirstOrDefault(t => t.TitleId == titleId);
                if (title == null) continue;
                foreach (var directorId in directors)
                {
                    var director = context.Persons.FirstOrDefault(p => p.PersonId == directorId);
                    if (director != null && !title.PersonsPeople.Contains(director))
                    {
                        title.PersonsPeople.Add(director);
                    }
                }
                foreach (var writerId in writers)
                {
                    var writer = context.Persons.FirstOrDefault(p => p.PersonId == writerId);
                    if (writer != null && !title.PersonsPeople1.Contains(writer))
                    {
                        title.PersonsPeople1.Add(writer);
                    }
                }



            }
            context.SaveChanges();
            Console.WriteLine($"Seeded first {noOfRow} crew records.");

        }
    }
}
