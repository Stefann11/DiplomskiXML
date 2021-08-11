using PostMicroservice.Core.Model;
using System.Collections.Generic;

namespace PostMicroservice.Core.Interface.Repository
{
    public interface ILocationRepository : IRepository<Location>
    {
        public IEnumerable<Location> GetByText(string text);

        public IEnumerable<Location> GetCountryByText(string text);

        public IEnumerable<Location> GetCityByText(string text);

        public IEnumerable<Location> GetStreetByText(string text);
    }
}