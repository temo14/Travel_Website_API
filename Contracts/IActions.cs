using Entities.DataTransferObjects;
using Entities.Helper;
using Entities.Models;

namespace Contracts
{
    public interface IActions
    {
        PagedList<SearcResultApartmentsDto> SearchApartments(SearchParameters search);
        void AddBook_Guest(BookingGuests service);
        void Updatebookings_guests(Guid id, string status);
        IEnumerable<ReturnGuestsDto>? GetGuests(Guid Id);
        IEnumerable<ReturnBookingsDto>? GetBookings(Guid Id);
    }
}
