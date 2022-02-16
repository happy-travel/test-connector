using HappyTravel.BaseConnector.Api.Services.Locations;
using HappyTravel.EdoContracts.GeoData.Enums;
using HappyTravel.TestConnector.Api.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Location = HappyTravel.EdoContracts.GeoData.Location;

namespace HappyTravel.TestConnector.Api.Services.Connector;

public class LocationService : ILocationService
{
    public LocationService(IOptions<AccommodationStorage> options)
    {
        _accommodationStorage = options.Value;
    }


    public Task<List<Location>> Get(DateTime modified, LocationTypes locationType, int skip, int top, CancellationToken cancellationToken)
    {
        var result = locationType switch
        {
            LocationTypes.Destination => new List<Location>(),
            LocationTypes.Accommodation => GetAccommodations(),
            LocationTypes.Landmark => new List<Location>(),
            LocationTypes.Location => GetLocalities(),
            _ => throw new ArgumentException($"Invalid location type {locationType}")
        };

        return Task.FromResult(result);


        List<Location> GetAccommodations()
        {
            return _accommodationStorage.Accommodations
                .Take(top)
                .Skip(skip)
                .Select(a => new Location(name: a.Name.En,
                    locality: a.Location.Locality?.En ?? string.Empty,
                    country: a.Location.Country.En,
                    coordinates: a.Location.Coordinates,
                    distance: default,
                    source: PredictionSources.Interior,
                    type: LocationTypes.Accommodation
                ))
                .ToList();
        }


        List<Location> GetLocalities()
        {
            return _accommodationStorage.Accommodations
                .Take(top)
                .Skip(skip)
                .Select(a => new Location(name: string.Empty,
                    locality: a.Location.Locality?.En ?? string.Empty,
                    country: a.Location.Country.En,
                    coordinates: a.Location.Coordinates,
                    distance: default,
                    source: PredictionSources.Interior,
                    type: LocationTypes.Accommodation
                ))
                .ToList();
        }
    }
    
    
    private readonly AccommodationStorage _accommodationStorage;
}