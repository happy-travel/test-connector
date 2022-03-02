using CSharpFunctionalExtensions;
using HappyTravel.BaseConnector.Api.Services.Availabilities.WideAvailabilities;
using HappyTravel.EdoContracts.Accommodations;
using HappyTravel.TestConnector.Api.Infrastructure.Options;
using HappyTravel.TestConnector.Api.Services.Supplier;
using Microsoft.Extensions.Options;

namespace HappyTravel.TestConnector.Api.Services.Connector;

public class WideAvailabilitySearchService : IWideAvailabilitySearchService
{
    public WideAvailabilitySearchService(IOptions<AccommodationStorage> options, ISupplierService supplierService, 
        IWideResultStorage resultStorage)
    {
        _supplierService = supplierService;
        _resultStorage = resultStorage;
        _accommodationStorage = options.Value;
    }

    
    public async Task<Result<Availability>> Get(AvailabilityRequest request, string languageCode, CancellationToken cancellationToken)
    {
        var availabilityId = Guid.NewGuid().ToString();
        var nights = (request.CheckOutDate - request.CheckInDate).Days;
        var ids = request.AccommodationIds
            .Intersect(_accommodationStorage.Accommodations.Select(a => a.SupplierCode))
            .ToList();

        var results = _supplierService.GetWideAvailabilityResult(availabilityId: availabilityId,
            accommodationIds: ids,
            checkInDate: request.CheckInDate.DateTime,
            occupancies: request.Rooms);

        var availability = new Availability(availabilityId: availabilityId,
            numberOfNights: nights,
            checkInDate: request.CheckInDate,
            checkOutDate: request.CheckOutDate,
            results: results,
            numberOfProcessedAccommodations: ids.Count);
        
        await _resultStorage.Set(availabilityId, availability);

        return availability;
    }


    private readonly AccommodationStorage _accommodationStorage;
    private readonly ISupplierService _supplierService;
    private readonly IWideResultStorage _resultStorage;
}