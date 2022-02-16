using HappyTravel.EdoContracts.Accommodations.Internals;
using HappyTravel.Money.Enums;
using HappyTravel.TestConnector.Api.Models;

namespace HappyTravel.TestConnector.Api.Services.Supplier;

public class SupplierService : ISupplierService
{
    public async Task<List<SlimAccommodationAvailability>> GetWideAvailabilityResult(string availabilityId, 
        List<string> accommodationIds, DateTime checkInDate, List<RoomOccupationRequest> occupancies)
    {
        var results = new List<SlimAccommodationAvailability>();

        foreach (var accommodationId in accommodationIds)
        {
            var availability = accommodationId switch
            {
                "HT1" => WideResultGenerator.Generate(availabilityId: availabilityId, 
                    accommodationId: accommodationId, 
                    checkinDate: checkInDate, 
                    occupancies: occupancies,
                    options: new GenerationOptions
                    {
                        AvailabilitiesCount = 5,
                        StartAmount = 10m,
                        AmountStep = 3m,
                        Currency = Currencies.USD,
                        DeadlineOffset = TimeSpan.FromDays(3),
                        CancellationPercentage = 100,
                        IsAdvancePurchaseRate = false
                    }),
                
                "HT2" => WideResultGenerator.Generate(availabilityId: availabilityId, 
                    accommodationId: accommodationId, 
                    checkinDate: checkInDate, 
                    occupancies: occupancies,
                    options: new GenerationOptions
                    {
                        AvailabilitiesCount = 5,
                        StartAmount = 10m,
                        AmountStep = 3m,
                        Currency = Currencies.AED,
                        DeadlineOffset = TimeSpan.FromDays(3),
                        CancellationPercentage = 100,
                        IsAdvancePurchaseRate = false
                    }),
                
                "HT3" => WideResultGenerator.Generate(availabilityId: availabilityId, 
                    accommodationId: accommodationId, 
                    checkinDate: checkInDate,
                    occupancies: occupancies,
                    options: new GenerationOptions
                    {
                        AvailabilitiesCount = 5,
                        StartAmount = 10m,
                        AmountStep = 3m,
                        Currency = Currencies.EUR,
                        DeadlineOffset = TimeSpan.FromDays(3),
                        CancellationPercentage = 100,
                        IsAdvancePurchaseRate = false
                    }),
                
                "HT4" => WideResultGenerator.Generate(availabilityId: availabilityId, 
                    accommodationId: accommodationId, 
                    checkinDate: checkInDate, 
                    occupancies: occupancies,
                    options: new GenerationOptions
                    {
                        AvailabilitiesCount = 5,
                        StartAmount = 10m,
                        AmountStep = 3m,
                        Currency = Currencies.USD,
                        DeadlineOffset = TimeSpan.FromDays(3),
                        CancellationPercentage = 100,
                        IsAdvancePurchaseRate = true
                    }),
                
                "HT5" => WideResultGenerator.Generate(availabilityId: availabilityId, 
                    accommodationId: accommodationId, 
                    checkinDate: checkInDate, 
                    occupancies: occupancies,
                    options: new GenerationOptions
                    {
                        AvailabilitiesCount = 5,
                        StartAmount = 10m,
                        AmountStep = 3m,
                        Currency = Currencies.AED,
                        DeadlineOffset = TimeSpan.FromDays(3),
                        CancellationPercentage = 100,
                        IsAdvancePurchaseRate = true
                    }),
                
                "HT6" => WideResultGenerator.Generate(availabilityId: availabilityId, 
                    accommodationId: accommodationId, 
                    checkinDate: checkInDate, 
                    occupancies: occupancies,
                    options: new GenerationOptions
                    {
                        AvailabilitiesCount = 5,
                        StartAmount = 10m,
                        AmountStep = 3m,
                        Currency = Currencies.EUR,
                        DeadlineOffset = TimeSpan.FromDays(3),
                        CancellationPercentage = 100,
                        IsAdvancePurchaseRate = true
                    }),
                
                "HT7" => WideResultGenerator.Generate(availabilityId: availabilityId, 
                    accommodationId: accommodationId, 
                    checkinDate: checkInDate, 
                    occupancies: occupancies,
                    options: new GenerationOptions
                    {
                        AvailabilitiesCount = 2000,
                        StartAmount = 10m,
                        AmountStep = 3m,
                        Currency = Currencies.USD,
                        DeadlineOffset = TimeSpan.FromDays(3),
                        CancellationPercentage = 100,
                        IsAdvancePurchaseRate = false
                    })
            };
            
            results.Add(availability);
        }

        return results;
    }
}