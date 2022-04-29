using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorApp.Api.Common.Data;

public interface ICosmosDbRepository<T> where T : Entity
{
    Task AddAsync(T entity, string partitionKey, CancellationToken cancellationToken);

    Task DeleteAsync(string id, string partitionKey, CancellationToken cancellationToken);

    Task<T> GetAsync(string id, string partitionKey, CancellationToken cancellationToken);

    Task<IEnumerable<T>> ListAsync(CancellationToken cancellationToken);

    Task UpdateAsync(T entity, string partitionKey, CancellationToken cancellationToken);
}

public class CosmosDbRepository<T> : ICosmosDbRepository<T> where T : Entity
{
    private readonly Container _container;

    public CosmosDbRepository(IConfiguration configuration, string partitionKey, ICollection<string> uniqueKeyPaths = null)
    {
        string account = configuration["CosmosDb_Account"];
        string containerName = configuration["CosmosDb_Container"];
        string key = configuration["CosmosDb_Key"];

        CosmosClient client = new(account, key);
        DatabaseResponse database = client.CreateDatabaseIfNotExistsAsync(containerName).GetAwaiter().GetResult();
        ContainerProperties properties = new(GetContainerName(), $"/{partitionKey}");

        _container = client.GetContainer(containerName, GetContainerName());

        if (uniqueKeyPaths != null)
        {
            UniqueKeyPolicy uniqueKeyPolicy = new();
            foreach (string uniqueKeyPath in uniqueKeyPaths)
            {
                UniqueKey uniqueKey = new();
                uniqueKey.Paths.Add(uniqueKeyPath);
                uniqueKeyPolicy.UniqueKeys.Add(uniqueKey);
            }
            properties.UniqueKeyPolicy = uniqueKeyPolicy;
        }

        database.Database.CreateContainerIfNotExistsAsync(properties).Wait();
    }

    public Task AddAsync(T entity, string partitionKey, CancellationToken cancellationToken)
    {
        return _container.CreateItemAsync<T>(entity, new PartitionKey(partitionKey), cancellationToken: cancellationToken);
    }

    public Task DeleteAsync(string id, string partitionKey, CancellationToken cancellationToken)
    {
        return _container.DeleteItemAsync<T>(id, new PartitionKey(partitionKey), cancellationToken: cancellationToken);
    }

    public async Task<T> GetAsync(string id, string partitionKey, CancellationToken cancellationToken)
    {
        try
        {
            ItemResponse<T> response = await _container.ReadItemAsync<T>(id, new PartitionKey(partitionKey), cancellationToken: cancellationToken);
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task<IEnumerable<T>> ListAsync(CancellationToken cancellationToken)
    {
        List<T> results = new();

        using (FeedIterator<T> query = _container.GetItemLinqQueryable<T>().ToFeedIterator())
        {
            while (query.HasMoreResults)
            {
                FeedResponse<T> response = await query.ReadNextAsync(cancellationToken);
                results.AddRange(response.ToList());
            }
        }

        return results;
    }

    public Task UpdateAsync(T entity, string partitionKey, CancellationToken cancellationToken)
    {
        return _container.UpsertItemAsync(entity, new PartitionKey(partitionKey), cancellationToken: cancellationToken);
    }

    private static string GetContainerName()
    {
        return typeof(T).Name.Replace("Entity", "");
    }
}