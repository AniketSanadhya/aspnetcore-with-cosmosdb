using DemoCarApp.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoCarApp.Services
{
    public class CarsService : ICarsService
    {
        private Container container;
        public CarsService(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task Add(Cars car)
        {
            await container.CreateItemAsync(car, new PartitionKey(car.Id.ToString()));
        }

        public async Task Delete(Guid id)
        {
            await container.DeleteItemAsync<Cars>(id.ToString(), new PartitionKey(id.ToString()));
        }

        public async Task<List<Cars>> GetCars()
        {
            try
            {
                var response = container.GetItemQueryIterator<Cars>(new QueryDefinition("SELECT * FROM Cars"));
                List<Cars> cars = new List<Cars>();
                while(response.HasMoreResults)
                {
                    var data = await response.ReadNextAsync();
                    cars.AddRange(data.ToList());
                }
                return cars;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Cars> GetCarById(Guid id)
        {
            try
            {
                var response = await container.ReadItemAsync<Cars>(id.ToString(), new PartitionKey(id.ToString()));
                return response.Resource;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task Update(Guid id)
        {
            try
            {

                var data = await GetCarById(id);
                if (data != null)
                {
                    await container.UpsertItemAsync<Cars>(data, new PartitionKey(id.ToString()));
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
