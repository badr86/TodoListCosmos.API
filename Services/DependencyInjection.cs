using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace TodoListCosmos.API.Services
{
    public static class DependencyInjection
    {
        public static async Task<IServiceCollection> AddCosmosDbService(this IServiceCollection collection, IConfiguration Configuration)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (Configuration == null) throw new ArgumentNullException(nameof(Configuration));

            var configurationSection = Configuration.GetSection("CosmosDb");
            var databaseName = configurationSection["DatabaseName"];
            var containerName = configurationSection["ContainerName"];
            var account = configurationSection["Account"];
            var key = configurationSection["Key"];

            var client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
            var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");

            var cosmosDbService = new CosmosDbService(client, databaseName, containerName);
            collection.AddSingleton<ICosmosDbService>(cosmosDbService);
            return collection;
        }
    }
}
