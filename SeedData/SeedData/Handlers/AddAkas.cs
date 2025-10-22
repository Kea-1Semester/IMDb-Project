using EfCoreModelsLib.Models.Mysql;
using Microsoft.EntityFrameworkCore;
using Attribute = EfCoreModelsLib.Models.Mysql.Attributes;
using Type = EfCoreModelsLib.Models.Mysql.Types;

namespace SeedData.Handlers;

public static class AddAkas
{
    public static async Task AddAkasToDb(ImdbContext context, string titleAkasPath, int noOfRow, Dictionary<string, Guid> titleIdsDict)
    {
        Console.WriteLine("Seed data in Aliases, Attributes or Types");

        bool anyAliases = await context.Aliases.AnyAsync();
        bool anyAttributes = await context.Attributes.AnyAsync();
        bool anyTypes = await context.Types.AnyAsync();

        if (!anyAliases && !anyAttributes && !anyTypes)
        {
            var aliases = new List<Aliases>();
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

                        var alias = new Aliases
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
                                Type = typeStr == "\\N" ? "unknown" : typeStr,
                            };

                            // Avoid adding duplicate types
                            if (!uniqueTypes.Add(type.Type)) continue;

                            types.Add(type);

                            // Optionally associate with alias if navigation property exists
                            alias.TypesType ??= new List<Type>(); // if is the list is null then create new list
                            alias.TypesType.Add(type);
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
                                Attribute = attr == "\\N" ? "unknown" : attr,
                            };
                            if (!uniqueAttributes.Add(attribute.Attribute)) continue;
                            attributes.Add(attribute);
                            alias.AttributesAttribute ??= new List<Attribute>();
                            alias.AttributesAttribute.Add(attribute);
                        }

                        count++;
                        if (count >= noOfRow) break;
                    }
                }
            }

            try
            {
                await context.Aliases.AddRangeAsync(aliases);
                Console.WriteLine($"Adding Aliases");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}: {ex.InnerException?.Message}");
            }

            try
            {
                await context.Types.AddRangeAsync(types);
                Console.WriteLine($"Adding Types");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}: {ex.InnerException?.Message}");
            }

            try
            {
                await context.Attributes.AddRangeAsync(attributes);
                Console.WriteLine($"Adding Attributes");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}: {ex.InnerException?.Message}");
            }

            try
            {
                await context.SaveChangesAsync();
                Console.WriteLine($"Saving Aliases, Attributes or Types");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}: {ex.InnerException?.Message}");
            }
        }
        else
        {
            Console.WriteLine("Database already have Aliases, Attributes or Types");
        }
    }
}