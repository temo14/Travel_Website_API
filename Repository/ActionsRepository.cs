using Contracts;
using Entities;
using Entities.DataTransferObjects;
using Entities.Models;
using System;
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

        public void AddBook_Guest(BookingGuests service)
        {
            var item = _context.BookingGuests.FirstOrDefault(i => i.HostId == service.HostId && i.status==(Status)1
                                                                && i.From < service.From && i.To > service.From);
            if (item == null)
                _context.BookingGuests.Add(service);
            else
                throw new Exception("Apartment already Booked");
        }

        public PagedList<SearcResultApartmentsDto> SearchApartments(SearchParameters search)
        {
            var apartments = _context.Apartments.AsQueryable();

            SearchByCity(ref apartments, search.City);

            bool checkDateRange = search.From != null && search.To != null;

            var result = from app in apartments.AsEnumerable()
                         from date in _context.BookingGuests
                         where date.HostId == app.OwnerId
                         select new SearcResultApartmentsDto
                         {

                             Address = app.Address,
                             City = app.City,
                             Description = app.Description,
                             DistanceFromCenter = app.DistanceFromCenter,
                             NumOfBeds = app.NumOfBeds,
                             OwnerId = app.OwnerId,
                             Image = app.Image,
                             Avaliable = checkDateRange ?
                                         !(date.From < search.From && date.To > search.From)
                                         && !(date.From < search.To && date.To > search.To)
                                         && !(date.From > search.From && date.To < search.To) : null
                         };

            if (checkDateRange && result != null)
            {
                result = result.OrderBy(x => x.Avaliable);

                result = result.DistinctBy(i => i.OwnerId);
            }

            // Sort
            Sort(ref result, search.OrderBy);

            // Filter with beds.
            result = result.AsQueryable().Where(i => i.NumOfBeds == search.Bedsfilter);

            return PagedList<SearcResultApartmentsDto>.ToPagedList(result,
                search.PageNumber, search.PageSize);
        }

        public IEnumerable<ReturnBookingsDto>? GetBookings(Guid Id)
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
                           Status = b.status
                       };
            }
            return null;
        }

        public IEnumerable<ReturnGuestsDto>? GetGuests(Guid Id)
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
                           guest = u
                       };
            }
            return null;
        }

        public void Updatebookings_guests(Guid id, string status)
        {
            var request = _context.BookingGuests.FirstOrDefault(i => i.Id == id);

            if (request == null) throw new NullReferenceException($"Id - {id} doesnot exists");

            if(status.Equals((Status)2))
            {
                _context.BookingGuests.Remove(request);
                return;
            }
            request.status = (Status)Enum.Parse(typeof(Status), status);

        }

        private void SearchByCity(ref IQueryable<Apartments> apartments, string? city)
        {
            if (!apartments.Any() || string.IsNullOrEmpty(city))
            {
                apartments = apartments.Where(x => x.City.StartsWith(city) || city == null);
            }

            apartments = apartments.Where(a => a.City.ToLower().Contains(city.Trim().ToLower()));
        }
        private void Sort(ref IEnumerable<SearcResultApartmentsDto> apartments, string? sortBy)
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
