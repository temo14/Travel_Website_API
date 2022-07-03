using Entities.DataTransferObjects;
using Entities.Helper;
using Entities.Models;

namespace Contracts
{
    public interface IActions
    {
        PagedList<SearcResultApartmentsDto> SearchApartments(SearchParameters search);
        void AddBookGuest(BookingGuests service);
        void UpdateBookingsGuests(UpdateStatus update);
        IEnumerable<ReturnGuestsDto>? GetGuests(Guid? Id);
        IEnumerable<ReturnBookingsDto>? GetBookings(Guid? Id);
    }
}
