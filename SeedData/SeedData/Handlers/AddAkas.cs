using SeedData.Models;
using Attribute = SeedData.Models.Attribute;
using Type = SeedData.Models.Type;

namespace SeedData.Handlers;

public static class AddAkas
{
    public static void AddAkasToDb(ImdbContext context, string titleAkasPath, int noOfRow)
    {
        Console.WriteLine("Seed data in AddAkas");
        var titleDict = context.Titles.ToDictionary(t => t.TitleId, t => t);
        //var typeDict = context.Types.ToDictionary(t => t.TypeId, t => t);

        var titleAkasLines = File.ReadAllLines(titleAkasPath)
            .Skip(1)
            .Select(line => line.Split('\t'))
            .Where(parts => parts.Length > 7);

        var aliases = new List<Alias>();
        var types = new List<Type>();
        var attributes = new List<Attribute>();
        // unique types to avoid duplicates
        var uniqueTypes = new HashSet<string>();
        var uniqueAttributes = new HashSet<string>();

        foreach (var parts in titleAkasLines.Take(noOfRow))
        {
            var aliasId = Guid.NewGuid();
            var titleId = parts[0];

            if (!titleDict.ContainsKey(titleId)) continue;
            
            var alias = new Alias
            {
                AliasId = aliasId,
                TitleId = titleId,
                Title = parts[2],
                Region = parts[3] != "\\N" ? parts[3] : "unknown",
                Language = parts[4] != "\\N" ? parts[4] : "unknown",
                IsOriginalTitle = parts[6] == "1"
            };
            aliases.Add(alias);

            // Normalize types (column 5)
            var typeStrings = parts[5].Split(',')
                .Select(s => s.Trim())
                .Where(s => !string.IsNullOrEmpty(s)).ToList();

            foreach (var typeStr in typeStrings
                         .Where(typeStr => !string.IsNullOrWhiteSpace(typeStr)))
            {
                var type = new Type
                {
                    TypeId = Guid.NewGuid(),
                    Type1 = typeStr == "\\N" ? "unknown" : typeStr,
                };
                // Avoid adding duplicate types
                if (!uniqueTypes.Add(type.Type1)) continue;
                types.Add(type);
                // Optionally associate with alias if navigation property exists
                alias.TypesTypes ??= new List<Type>(); // if is the list is null then create new list
                alias.TypesTypes.Add(type);
            }

            var attributeString = parts[6].Split(',')
                .Select(s => s.Trim())
                .Where(s => !string.IsNullOrEmpty(s))
                .ToList();

            foreach (var attr in attributeString.Where(attr => attr == "\\N"))
            {
                var attribute = new Attribute
                {
                    AttributeId = Guid.NewGuid(),
                    Attribute1 = attr == "\\N" ? "unknown" : attr,
                };
                if (!uniqueAttributes.Add(attribute.Attribute1)) continue;
                attributes.Add(attribute);
                alias.AttributesAttributes ??= new List<Attribute>();
                alias.AttributesAttributes.Add(attribute);
            }
        }

        context.Aliases.AddRange(aliases);
        context.Types.AddRange(types);
        context.Attributes.AddRange(attributes);
        context.SaveChanges();
        Console.WriteLine($"Seeded first {noOfRow} Akas records.");
    }

}