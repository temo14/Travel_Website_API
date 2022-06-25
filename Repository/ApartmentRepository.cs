using Contracts;
using Entities;
using Entities.Helper;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    internal class ApartmentRepository :RepositoryBase<Apartments>, IApartmentRepository
    {
        public ApartmentRepository(RepositoryContext context) : base(context)
        {

        }

        public void AddApartment(Apartments appartments) => Create(appartments);

        public ApartmentDetails GetApartmentDetails(Guid appartmentId)
        {
            var apartment = RepositoryContext.Apartments.FirstOrDefault(x => x.Id == appartmentId);
            if (apartment == null) throw new ArgumentNullException("Apartment doesnot exists");

            var avaliabilty = from bg in RepositoryContext.BookingGuests
                              where bg.HostId == apartment.OwnerId && bg.status == Status.Accepted
                              join guests in RepositoryContext.Users on bg.GuestId equals guests.Id
                              select new AvaliabilityInfo
                              {
                                  Guest = guests,
                                  From = bg.From,
                                  To = bg.To
                              };
            return new ApartmentDetails() { Apartment = apartment, Avalibilities = avaliabilty };
        }

        public void UpdateApartment(Apartments appartments) => Update(appartments);
    }
}
