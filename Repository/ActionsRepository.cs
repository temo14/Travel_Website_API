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

            throw new Exception("Invalid date");
        }

        public PagedList<SearcResultAppartmentsDto> GetAppartments(SearchParameters search)
        {
            var result = from app in _context.Appartments
                         from date in _context.BookingGuests
                         where date.HostId == app.OwnerId
                         orderby date.From < search.From && date.To > search.From
                         select new SearcResultAppartmentsDto
                         {
                             Address = app.Address,
                             City = app.City,
                             Description = app.Description,
                             DistanceFromCenter = app.DistanceFromCenter,
                             NumOfBeds = app.NumOfBeds,
                             OwnerId = app.OwnerId,
                             PhotoLocation = app.PhotoLocation,
                             Avaliable = date.From < search.From && date.To > search.From
                         };

            SearchByCity(ref result, search.City);



            return PagedList<SearcResultAppartmentsDto>.ToPagedList(result,
                search.PageNumber, search.PageSize);
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
            var request = _context.BookingGuests.FirstOrDefault(i => i.Id == id);

            if (request == null) throw new NullReferenceException($"Id - {id} doesnot exists");

            if(status.Equals((Status)2))
            {
                _context.BookingGuests.Remove(request);
                return;
            }
            request.status = (Status)Enum.Parse(typeof(Status), status);

        }

        private void SearchByCity(ref IQueryable<SearcResultAppartmentsDto> apps, string? city)
        {
            if (!apps.Any() || string.IsNullOrEmpty(city))
            {
                apps = apps.Where(x => x.City.StartsWith(city) || city == null);
            }

            apps = apps.Where(a => a.City.ToLower().Contains(city.Trim().ToLower())) ;
        }

    }
}
