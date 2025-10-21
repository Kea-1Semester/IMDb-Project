using SeedData.Models;
using Attribute = SeedData.Models.Attribute;
using Type = SeedData.Models.Type;

namespace SeedData.Handlers;

public static class AddAkas
{
    public static async Task AddAkasToDb(ImdbContext context, string titleAkasPath, int noOfRow, Dictionary<string, Guid> titleIdsDict)
    {
        Console.WriteLine("Seed data in AddAkas");

        var aliases = new List<Alias>();
        var types = new List<Type>();
        var attributes = new List<Attribute>();

        // unique types to avoid duplicates
        var uniqueTypes = new HashSet<string>();
        var uniqueAttributes = new HashSet<string>();

        using (var stream = new FileStream(titleAkasPath, FileMode.Open, FileAccess.Read, FileShare.Read, 65536, FileOptions.Asynchronous))
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

                    if (!titleIdsDict.TryGetValue(tconst, out var titleId)) continue;

                    var alias = new Alias
                    {
                        AliasId = Guid.NewGuid(),
                        TitleId = titleId,
                        Title = columns[2],
                        Region = columns[3] != "\\N" ? columns[3] : "unknown",
                        Language = columns[4] != "\\N" ? columns[4] : "unknown",
                        IsOriginalTitle = columns[6] == "1"
                    };

                    aliases.Add(alias);

                    // Normalize types (column 5)
                    var typeStrings = columns[5]
                        .Split(',')
                        .Select(s => s.Trim())
                        .Where(s => !string.IsNullOrEmpty(s))
                        .ToList();

                    foreach (var typeStr in typeStrings.Where(typeStr => !string.IsNullOrWhiteSpace(typeStr)))
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

                    var attributeString = columns[6]
                        .Split(',')
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

                    count++;
                    if (count >= noOfRow) break;
                }
            }
        }

        await context.Aliases.AddRangeAsync(aliases);
        await context.Types.AddRangeAsync(types);
        await context.Attributes.AddRangeAsync(attributes);
        await context.SaveChangesAsync();

        Console.WriteLine($"Seeded first {noOfRow} Akas records.");
    }
}