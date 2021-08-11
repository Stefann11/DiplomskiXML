using PostMicroservice.Core.Model;
using System;
using System.Data;

namespace PostMicroservice.DataAccess.Adaptee
{
    public class LocationAdaptee
    {
        public Location ConvertSqlDataReaderToLocation(DataRow dataRow)
        {
            return Location.Create(id: Guid.Parse(dataRow[0].ToString().Trim()),
                                   street: Street.Create(dataRow[1].ToString().Trim()).Value,
                                   cityName: CityName.Create(dataRow[2].ToString().Trim()).Value,
                                   country: Country.Create(dataRow[3].ToString().Trim()).Value).Value;
        }
    }
}