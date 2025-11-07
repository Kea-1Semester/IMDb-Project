using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Neo4j.Driver;
using EfCoreModelsLib.models.Neo4J.Neo4JModels;
using EfCoreModelsLib.models.Neo4J.Handler;
using DotNetEnv;

namespace SeedData.Handlers.Neo4j;
 {
    public static class Neo4jMapper
    {
        public static async Task MigrateToNeo4j(
            IEnumerable<Attribute> source,
            int batchSize = 1000)
        {
            
        }
    }
 }