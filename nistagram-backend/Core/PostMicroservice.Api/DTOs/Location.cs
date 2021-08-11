using System;

namespace PostMicroservice.Api.DTOs
{
    public class Location
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string CityName { get; set; }
        public string Country { get; set; }
    }
}