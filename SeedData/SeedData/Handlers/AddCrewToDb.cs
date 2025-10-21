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
        public static async Task AddCrew(ImdbContext context, string path, int noOfRow, Dictionary<string, Guid> titleIdsDict, Dictionary<string, Guid> personIdsDict)
        {
            //Directors , writers 
            Console.WriteLine("Seed data in AddCrew");

            var titlesDict = context.Titles.ToDictionary(t => t.TitleId, t => t);
            var personsDict = context.Persons.ToDictionary(p => p.PersonId, p => p);

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 65536, FileOptions.Asynchronous))
            {
                using (var reader = new StreamReader(stream))
                {
                    await reader.ReadLineAsync(); // Skip header line

                    string? line;
                    int count = 0;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        var columns = line.Split('\t');
                        
                        var tconst = columns[0];

                        var title = titleIdsDict.TryGetValue(tconst, out var titleGuid) && titlesDict.TryGetValue(titleGuid, out var _title)
                            ? _title
                            : null;

                        var directors = columns[1]
                            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                        
                        var writers = columns[2]
                            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                        
                        foreach (var directorId in directors)
                        {
                            if (personIdsDict.TryGetValue(directorId, out var guid) && personsDict.TryGetValue(guid, out var person))
                            {
                                title?.PersonsPeople.Add(person);
                            }
                        }

                        foreach (var writerId in writers)
                        {
                            if (personIdsDict.TryGetValue(writerId, out var guid) && personsDict.TryGetValue(guid, out var person))
                            {
                                title?.PersonsPeople1.Add(person);
                            }
                        }

                        count++;
                        if (count >= noOfRow)
                        {
                            break;
                        }
                    }
                }
            }

            await context.SaveChangesAsync();

            Console.WriteLine($"Seeded first {noOfRow} crew records.");
        }
    }
}
