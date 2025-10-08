using System;
using System.IO;
using System.Linq;
using SeedData.Models;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        string projectRoot = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName;
        string dataFolder = Path.Combine(projectRoot, "data");
        string titleBasicPath = Path.Combine(dataFolder, "title.basics.tsv");

        var titleBasics = new List<TitleBasic>();
        var titleGenres = new List<TitleGenre>();

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
                StartYear = DateOnly.TryParse(columns[5], out var startYear) ? startYear : default,
                EndYear = DateOnly.TryParse(columns[6], out var endYear) ? endYear : null,
                RuntimeMinutes = int.TryParse(columns[7], out var runtime) ? runtime : null
            };
            titleBasics.Add(titleBasic);
        }

        using var context = new ImdbContext();
        context.TitleBasics.AddRange(titleBasics);
        //context.TitleGenres.AddRange(titleGenres);
        context.SaveChanges();

        Console.WriteLine("Seeded first 50,000 TitleBasic and TitleGenre records.");
    }
}