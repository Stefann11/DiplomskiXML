using CSharpFunctionalExtensions;
using MongoDB.Driver;
using StoryMicroservice.Core.DTOs;
using StoryMicroservice.Core.Interface.Repository;
using StoryMicroservice.DataAccess.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryMicroservice.DataAccess.Implementation
{
    public class LocationRepository : ILocationRepository
    {
        private readonly IMongoCollection<Location> _locations;
        private readonly LocationFactory locationFactory;

        public LocationRepository(IStoryDatabaseSettings settings, LocationFactory locationFactory)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _locations = database.GetCollection<Location>(settings.LocationsCollectionName);
            this.locationFactory = locationFactory;
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Core.Model.Location Edit(string id, Core.Model.Location obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Core.Model.Location> GetAll()
        {
            return locationFactory.CreateLocations(_locations.Find(location => true).ToList());
        }

        public Maybe<Core.Model.Location> GetById(Guid id)
        {
            return locationFactory.Create(_locations.Find(location => location.Id.Equals(id.ToString())).FirstOrDefault());
        }

        public Core.Model.Location Save(Core.Model.Location obj)
        {
            throw new NotImplementedException();
        }
    }
}