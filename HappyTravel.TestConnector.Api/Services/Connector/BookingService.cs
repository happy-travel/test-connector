using CSharpFunctionalExtensions;
using HappyTravel.BaseConnector.Api.Services.Bookings;
using HappyTravel.EdoContracts.Accommodations;
using Microsoft.AspNetCore.Mvc;

namespace HappyTravel.TestConnector.Api.Services.Connector;

public class BookingService : IBookingService
{
    public async Task<Result<Booking, ProblemDetails>> Book(BookingRequest bookingRequest, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Booking>> Get(string referenceCode, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> Cancel(string referenceCode, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}