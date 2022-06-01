using CSharpFunctionalExtensions;
using HappyTravel.BaseConnector.Api.Infrastructure;
using HappyTravel.BaseConnector.Api.Services.Bookings;
using HappyTravel.EdoContracts.Accommodations;
using HappyTravel.EdoContracts.Accommodations.Enums;
using HappyTravel.TestConnector.Api.Models;
using HappyTravel.TestConnector.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HappyTravel.TestConnector.Api.Services.Connector;

public class BookingService : IBookingService
{
    public BookingService(TestConnectorContext context, IWideResultStorage resultStorage, IOptionsMonitor<Dictionary<string, GenerationOptions>> optionsStorage)
    {
        _context = context;
        _resultStorage = resultStorage;
        _optionsStorage = optionsStorage;
    }

    
    public async Task<Result<Booking, ProblemDetails>> Book(BookingRequest bookingRequest, CancellationToken cancellationToken)
    {
        return await GetCachedData()
            .Bind(SaveBooking)
            .Bind(Convert);


        async Task<Result<Availability, ProblemDetails>> GetCachedData()
        {
            var (_, isFailure, availability, error) = await _resultStorage.Get(bookingRequest.AvailabilityId);

            return isFailure
                ? ProblemDetailsBuilder.CreateFailureResult<Availability>(error, BookingFailureCodes.ConnectorValidationFailed)
                : availability;
        }


        async Task<Result<Data.Models.Booking, ProblemDetails>> SaveBooking(Availability availability)
        {
            var slimAccommodation = availability.Results
                .SingleOrDefault(a => a.RoomContractSets.Any(r => r.Id == bookingRequest.RoomContractSetId));
            
            var roomContractSet = slimAccommodation.RoomContractSets
                .SingleOrDefault(r => r.Id == bookingRequest.RoomContractSetId);
            
            if (roomContractSet.Equals(default))
                return ProblemDetailsBuilder.CreateFailureResult<Data.Models.Booking>("Cached result not found", BookingFailureCodes.ConnectorValidationFailed);

            if (!_optionsStorage.CurrentValue.TryGetValue(slimAccommodation.AccommodationId, out var options))
                return ProblemDetailsBuilder.CreateFailureResult<Data.Models.Booking>("Options not found", BookingFailureCodes.ConnectorValidationFailed);
            
            if (options.IsVccRequired && bookingRequest.CreditCard is null)
                return ProblemDetailsBuilder.CreateFailureResult<Data.Models.Booking>("VCC is required", BookingFailureCodes.ConnectorValidationFailed);
            
            var booking = new Data.Models.Booking
            {
                ReferenceCode = bookingRequest.ReferenceCode,
                AccommodationId = slimAccommodation.AccommodationId,
                CheckInDate = availability.CheckInDate,
                CheckOutDate = availability.CheckOutDate,
                Rooms = bookingRequest.Rooms,
                Status = options.BookingStatus
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync(cancellationToken);

            return booking;
        }


        Result<Booking, ProblemDetails> Convert(Data.Models.Booking booking)
            => this.Convert(booking);
    }

    public async Task<Result<Booking>> Get(string referenceCode, CancellationToken cancellationToken)
    {
        return await GetBooking(referenceCode)
            .Map(Convert);
    }

    public async Task<Result> Cancel(string referenceCode, CancellationToken cancellationToken)
    {
        return await GetBooking(referenceCode)
            .Tap(CancelBooking);


        async Task CancelBooking(Data.Models.Booking booking)
        {
            booking.Status = BookingStatusCodes.Cancelled;
            _context.Update(booking);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }


    private async Task<Result<Data.Models.Booking>> GetBooking(string referenceCode)
    {
        var booking = await _context.Bookings.SingleOrDefaultAsync(b => b.ReferenceCode == referenceCode);
        return booking ?? Result.Failure<Data.Models.Booking>($"Booking with reference code {referenceCode} not found");
    }
    
    
    private Booking Convert(Data.Models.Booking booking)
    {
        return new Booking(referenceCode: booking.ReferenceCode,
            status: booking.Status,
            accommodationId: booking.AccommodationId,
            supplierReferenceCode: booking.ReferenceCode,
            checkInDate: booking.CheckInDate.DateTime,
            checkOutDate: booking.CheckOutDate.DateTime,
            rooms: booking.Rooms,
            bookingUpdateMode: BookingUpdateModes.Synchronous,
            specialValues: new(0));
    }


    private readonly TestConnectorContext _context;
    private readonly IWideResultStorage _resultStorage;
    private readonly IOptionsMonitor<Dictionary<string, GenerationOptions>> _optionsStorage;
}