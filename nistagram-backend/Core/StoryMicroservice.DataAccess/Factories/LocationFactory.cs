using StoryMicroservice.Core.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Location = StoryMicroservice.Core.DTOs.Location;

namespace StoryMicroservice.DataAccess.Factories
{
    public class LocationFactory
    {
        public Location Create(Core.Model.Location location)
        {
            return new Location
            {
                Id = location.Id.ToString(),
                Country = location.Country,
                CityName = location.CityName,
                Street = location.Street
            };
        }

        public Core.Model.Location Create(Location location)
        {
            return Core.Model.Location.Create(new Guid(location.Id), Street.Create(location.Street).Value, CityName.Create(location.CityName).Value,
                Country.Create(location.Country).Value).Value;
        }

        public IEnumerable<Core.Model.Location> CreateLocations(List<Location> locations)
        {
            return locations.Select(location => Core.Model.Location.Create(new Guid(location.Id), Street.Create(location.Street).Value,
                CityName.Create(location.CityName).Value, Country.Create(location.Country).Value).Value);
        }

        public List<Location> CreateLocations(IEnumerable<Core.Model.Location> locations)
        {
            return locations.Select(location => Create(location)).ToList();
        }
    }
}