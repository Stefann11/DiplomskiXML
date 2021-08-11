using PostMicroservice.Api.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace PostMicroservice.Api.Factories
{
    public class LocationFactory
    {
        public Location Create(Core.Model.Location location)
        {
            return new Location
            {
                Id = location.Id,
                Street = location.Street,
                CityName = location.CityName,
                Country = location.Country
            };
        }

        public IEnumerable<Location> CreateLocations(IEnumerable<Core.Model.Location> locations)
        {
            return locations.Select(location => Create(location)).ToList();
        }

        public void CreateLocationsFromCountryQuery(IEnumerable<Core.Model.Location> queryResult, List<Location> locations)
        {
            if (queryResult.Any())
            {
                AddUniqueCountries(queryResult, locations);
                AddUniqueCities(queryResult, locations);
                AddUniqueStreets(queryResult, locations);
            }
        }

        public void CreateLocationsFromCityQuery(IEnumerable<Core.Model.Location> queryResult, List<Location> locations)
        {
            if (queryResult.Any())
            {
                AddUniqueCities(queryResult, locations);
                AddUniqueStreets(queryResult, locations);
            }
        }

        public void CreateLocationsFromStreetQuery(IEnumerable<Core.Model.Location> queryResult, List<Location> locations)
        {
            if (queryResult.Any())
            {
                AddUniqueStreets(queryResult, locations);
            }
        }

        private void AddUniqueCountries(IEnumerable<Core.Model.Location> queryResult, List<Location> locations)
        {
            queryResult.ToList().ForEach(location =>
            {
                var locationToAdd = new Location() { Country = location.Country, CityName = "", Street = "" };
                if (!locations.Any(l => l.Country.Equals(locationToAdd.Country))) { locations.Add(locationToAdd); }
            });
        }

        private void AddUniqueCities(IEnumerable<Core.Model.Location> queryResult, List<Location> locations)
        {
            queryResult.ToList().ForEach(location =>
            {
                var locationToAdd = new Location() { Country = location.Country, CityName = location.CityName, Street = "" };
                if (!locations.Any(l => l.CityName.Equals(locationToAdd.CityName))) { locations.Add(locationToAdd); }
            });
        }

        private void AddUniqueStreets(IEnumerable<Core.Model.Location> queryResult, List<Location> locations)
        {
            queryResult.ToList().ForEach(location =>
            {
                var locationToAdd = new Location()
                {
                    Country = location.Country,
                    CityName = location.CityName,
                    Street = location.Street
                };
                if (!locations.Any(l => l.CityName.Equals(locationToAdd.CityName) && (l.GetType().GetProperty("Street") == null
                || l.Street.Equals(locationToAdd.Street))))
                { locations.Add(locationToAdd); }
            });
        }
    }
}