using Bogus;
using Microsoft.EntityFrameworkCore;
using SeedData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedData.Handlers
{
    public static class SeedDataHandler
    {
        public static async Task SeedAsync(DbContext context, CancellationToken cancellationToken, int seed)
        {
            // Check if the Titles table is empty
            if (!(await context.Set<Title>().AnyAsync(cancellationToken)))
            {
                var titleFaker = new Faker<Title>().UseSeed(seed)
                    .RuleFor(t => t.TitleType, f => f.PickRandom(new List<string> { "Movie", "TvSeries", "TvShow" }))
                    .RuleFor(t => t.PrimaryTitle, f => f.Lorem.Lines(1))
                    .RuleFor(t => t.OriginalTitle, f => f.Lorem.Lines(1))
                    .RuleFor(t => t.IsAdult, f => f.Random.Bool())
                    .RuleFor(t => t.StartYear, f => f.Date.BetweenDateOnly
                    (
                        new DateOnly(1900, 1, 1),
                        DateOnly.FromDateTime(DateTime.Now)
                    )
                    .Year)
                    .RuleFor(t => t.EndYear, (f,e) => (e.TitleType == "Movie" ? null : f.Random.Int(e.StartYear, DateTime.Now.Year).OrNull(f, .5f)))
                    .RuleFor(t => t.RuntimeMinutes, f => f.Random.Number(1, 10000).OrNull(f, .5f));

                var titles = titleFaker.Generate(1000);

                await context.Set<Title>().AddRangeAsync(titles, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
