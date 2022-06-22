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

        public void AddBook_Guest(BookingGuests service) => _context.BookingGuests.Add(service);
        

        public IEnumerable<Appartments> GetAppartments(SearchParameters search)
        {
            var appartments = from a in _context.Appartments
                     from d in _context.BookingGuests
                     where d.From > search.From && d.To < search.To && d.HostId == a.OwnerId
                     select a;

            SearchByCity(ref appartments, search.City);

            return _context.Appartments
                .OrderBy(ci => ci.City == search.City)
                .Skip((search.PageNumber - 1) * search.PageSize)
                .Take(search.PageSize)
                .ToList();
        }

        public IEnumerable<ReturnBookingsDto>? GetBookings(Guid Id)
        {
            var bookings = _context.BookingGuests.Where(o => o.GuestId == Id);
            if (bookings != null)
            {
                return from a in _context.Appartments.AsEnumerable()
                       join b in bookings
                       on a.OwnerId equals b.HostId
                       select new ReturnBookingsDto()
                       {
                           appartment = a,
                           From = b.From,
                           PhotoUrl = a.PhotoLocation,
                           To = b.To,
                           status = b.status
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
            var item = _context.BookingGuests.FirstOrDefault(i => i.Id == id);

        }

        private void SearchByCity(ref IQueryable<Appartments> apps, string? city)
        {
            if (!apps.Any() || string.IsNullOrEmpty(city)) return;

            apps = apps.Where(a => a.City.ToLower().Contains(city.Trim().ToLower()));
        }
    }
}
