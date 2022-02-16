using HappyTravel.EdoContracts.Accommodations;
using HappyTravel.EdoContracts.Accommodations.Enums;
using HappyTravel.EdoContracts.Accommodations.Internals;
using HappyTravel.EdoContracts.General;
using HappyTravel.EdoContracts.General.Enums;
using HappyTravel.Money.Extensions;
using HappyTravel.Money.Models;
using HappyTravel.TestConnector.Api.Models;

namespace HappyTravel.TestConnector.Api.Services.Supplier;

public static class WideResultGenerator
{
    public static SlimAccommodationAvailability Generate(string availabilityId, string accommodationId, DateTime checkinDate, 
        List<RoomOccupationRequest> occupancies, GenerationOptions options)
    {
        return new SlimAccommodationAvailability(availabilityId: availabilityId,
            accommodationId: accommodationId,
            roomContractSets: Generate(checkinDate, occupancies, options));
    }


    private static List<RoomContractSet> Generate(DateTime checkinDate, List<RoomOccupationRequest> occupancies, GenerationOptions options)
    {
        var result = new List<RoomContractSet>();
        
        foreach (var i in Enumerable.Range(0, options.AvailabilitiesCount))
        {
            var amount = options.StartAmount + i * options.AmountStep;
            var deadlineDate = checkinDate.Add(options.DeadlineOffset);
            var rooms = occupancies
                .Select(o => new RoomContract(boardBasis: BoardBasisTypes.NotSpecified,
                    mealPlan: string.Empty,
                    contractTypeCode: i,
                    isAvailableImmediately: true,
                    isDynamic: false,
                    contractDescription: string.Empty,
                    remarks: new List<KeyValuePair<string, string>>(),
                    dailyRoomRates: new List<DailyRate>(),
                    rate: new Rate(finalPrice: amount.ToMoneyAmount(options.Currency),
                        gross: amount.ToMoneyAmount(options.Currency)),
                    adultsNumber: o.AdultsNumber,
                    childrenAges: o.ChildrenAges))
                .ToList();
            var finalAmount = rooms.Sum(r => r.Rate.FinalPrice.Amount);
            
            result.Add(new RoomContractSet(id: Guid.NewGuid(), 
                rate: new Rate(finalPrice: finalAmount.ToMoneyAmount(options.Currency),
                    gross: finalAmount.ToMoneyAmount(options.Currency)),
                deadline: new Deadline(deadlineDate, new List<CancellationPolicy>
                {
                    new(deadlineDate, options.CancellationPercentage)
                }),
                rooms: rooms,
                tags: new List<string>(),
                isDirectContract: false,
                isAdvancePurchaseRate: options.IsAdvancePurchaseRate,
                isPackageRate: false));
        }

        return result;
    }
}