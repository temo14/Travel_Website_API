using Contracts;
using Entities;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.Helper;

namespace Repository
{
    public class ActionsRepository : IActions
    {
        private readonly RepositoryContext _context;

        public ActionsRepository(RepositoryContext context)
        {
            _context = context;
        }

        public void AddBookGuest(BookingGuests service)
        {
            var item = _context.BookingGuests.FirstOrDefault(i => i.HostId == service.HostId && i.status == Status.Accepted
                                                                && i.From <= service.To && service.From <= i.To);
            if (item == null)
                _context.BookingGuests.Add(service);
            else
                throw new Exception("Apartment already Booked");
        }

        public PagedList<SearcResultApartmentsDto> SearchApartments(SearchParameters search)
        {
            var result = from app in _context.Apartments
                     select new SearcResultApartmentsDto
                     {
                         Id = app.Id,
                         Address = app.Address,
                         City = app.City,
                         Description = app.Description,
                         DistanceFromCenter = app.DistanceFromCenter,
                         NumOfBeds = app.NumOfBeds,
                         OwnerId = app.OwnerId,
                         Image = app.Image,
                         Avaliable = null
                     };

            SearchByCity(ref result, search.City);

            // Filter with beds.
            if (search.Bedsfilter > 0)
            {
                result = result.Where(i => i.NumOfBeds == search.Bedsfilter);
            }

            bool checkDateRange = search.From != null && search.To != null;
            /*date.HostId == app.OwnerId && date.status == Status.Accepted*/

            List<SearcResultApartmentsDto> list = new();

            if (checkDateRange)
            {
                foreach (var item in result)
                {
                    var bookedCase = _context.BookingGuests.FirstOrDefault(x => x.HostId == item.OwnerId
                        && x.status == Status.Accepted
                        && x.From <= search.To && search.From <= x.To);

                    item.Avaliable = bookedCase == null;

                    list.Add(item);
                }
                result = result.OrderBy(x => x.Avaliable);
                result = list.AsQueryable();
            }

            // Sort
            Sort(ref result, search.OrderBy);

            return PagedList<SearcResultApartmentsDto>.ToPagedList(result,
                search.PageNumber, search.PageSize);
        }

        public IEnumerable<ReturnBookingsDto>? GetBookings(Guid? Id)
        {
            var bookings = _context.BookingGuests.Where(o => o.GuestId == Id);
            if (bookings != null)
            {
                return from a in _context.Apartments.AsEnumerable()
                       join b in bookings
                       on a.OwnerId equals b.HostId
                       select new ReturnBookingsDto()
                       {
                           Apartment = a,
                           From = b.From,
                           Image = a.Image,
                           To = b.To,
                           Status = b.status.ToString()
                       };
            }
            return null;
        }

        public IEnumerable<ReturnGuestsDto>? GetGuests(Guid? Id)
        {
            var guests = _context.BookingGuests.Where(o => o.HostId == Id).ToList();
            if (guests != null)
            {
                return from g in guests
                       join u in _context.Users.AsEnumerable()
                       on g.GuestId equals u.Id
                       select new ReturnGuestsDto()
                       {
                           Id = g.Id,
                           From = g.From,
                           To = g.To,
                           Description = u.Description,
                           Email = u.Email,
                           FirstName = u.FirstName,
                           LastName = u.LastName,
                           Image = u.Image,
                           Status = g.status.ToString()
                       };
            }
            return null;
        }

        public void UpdateBookingsGuests(UpdateStatus update)
        {
            var request = _context.BookingGuests.FirstOrDefault(i => i.Id == update.id);
            if (request == null || update.Status == null) throw new NullReferenceException($"Invaild update request");

            // Check if apartment is already booked
            if (update.Status == Status.Accepted.ToString())
            {
                var checkDate = _context.BookingGuests.FirstOrDefault(x => x.status == Status.Accepted
               && x.From <= request.To && request.From <= x.To) ?? throw new Exception("Date Already Booked");
            }


            request.status = (Status)Enum.Parse(typeof(Status), update.Status);

        }

        private void SearchByCity(ref IQueryable<SearcResultApartmentsDto> apartments, string? city)
        {
            if (!apartments.Any() || string.IsNullOrEmpty(city))
            {
                apartments = apartments.Where(x => x.City.StartsWith(city) || city == null);
            }

            apartments = apartments.Where(a => a.City.ToLower().Contains(city.Trim().ToLower()));
        }
        private void Sort(ref IQueryable<SearcResultApartmentsDto> apartments, string? sortBy)
        {
            switch (sortBy)
            {
                case "NumOfBeds desc":
                    apartments = apartments.OrderByDescending(x => x.NumOfBeds);
                    break;
                case "DistanceFromCenter desc":
                    apartments.OrderByDescending(x => x.DistanceFromCenter);
                    break;
                case "DistanceFromCenter":
                    apartments= apartments.OrderBy(x => x.DistanceFromCenter);
                    break;
                case "NumOfBeds":
                    break;
                default:
                    break;
            }
        }
    }
}
