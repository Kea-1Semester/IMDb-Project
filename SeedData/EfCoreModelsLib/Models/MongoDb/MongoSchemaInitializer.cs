using MongoDB.Bson;
using MongoDB.Driver;

namespace EfCoreModelsLib.Models.MongoDb
{
    /// <summary>
    /// Utility for ensuring MongoDB collection schema and indexes.
    /// </summary>
    public static class MongoSchemaInitializer<T>
    {        
        /// <summary>
        /// Ensures the collection exists with the specified schema and indexes.
        /// </summary>
        public static async Task EnsureCollectionSchema(
            string connectionString,
            string databaseName,
            string collectionName,
            BsonDocument schema,
            IEnumerable<BsonDocument>? compoundIndex = null,
            BsonDocument? singleFieldIndex = null,
            CancellationToken cancellationToken = default
        )
        {
            try
            {
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase(databaseName);


                var collectionList = await database.ListCollectionNames().ToListAsync(cancellationToken);
                if (!collectionList.Contains(collectionName))
                {
                    var options = new CreateCollectionOptions<BsonDocument>()
                    {
                        Validator = new BsonDocumentFilterDefinition<BsonDocument>(schema)
                    };
                    await database.CreateCollectionAsync(collectionName, options, cancellationToken);

                    Console.WriteLine($"Created collection '{collectionName}' with schema validation.");
                }
                else
                {
                    var command = new BsonDocument
                    {
                        { "collMod", collectionName },
                        { "validator", schema }
                    };
                    await database.RunCommandAsync<BsonDocument>(command, cancellationToken: cancellationToken);
                }

                if (compoundIndex != null)
                {
                    var collection = database.GetCollection<T>(collectionName);
                    var indexes = compoundIndex
                        .Select(indexDoc =>
                            new CreateIndexModel<T>(new BsonDocumentIndexKeysDefinition<T>(indexDoc)))
                        .ToList();

                    if (indexes.Count > 0)
                        await collection.Indexes.CreateManyAsync(indexes, cancellationToken);
                    
                    Console.WriteLine($"Ensured indexes on collection '{collectionName}'.");
                    
                }
                if (singleFieldIndex != null)
                {
                    var collection = database.GetCollection<T>(collectionName);
                    var indexModel = new CreateIndexModel<T>(new BsonDocumentIndexKeysDefinition<T>(singleFieldIndex));
                    await collection.Indexes.CreateOneAsync(indexModel, cancellationToken: cancellationToken);

                    Console.WriteLine($"Ensured single field index on collection '{collectionName}'.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error ensuring collection schema for '{collectionName}': {ex.Message}", ex);
            }
        }
    }
}

