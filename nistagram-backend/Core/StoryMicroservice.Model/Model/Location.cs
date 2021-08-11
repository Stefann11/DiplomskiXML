using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryMicroservice.Core.Model
{
    public class Location
    {
        public Guid Id { get; }
        public Street Street { get; }
        public CityName CityName { get; }
        public Country Country { get; }

        private Location(Guid id, Street street, CityName cityName, Country country)
        {
            Id = id;
            CityName = cityName;
            Street = street;
            Country = country;
        }

        public static Result<Location> Create(Guid id, Street street, CityName cityName, Country country)
        {
            return Result.Success(new Location(id, street, cityName, country));
        }
    }
}