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
    internal class AppartmentRepository :RepositoryBase<Appartments>, IAppartmentRepository
    {
        public AppartmentRepository(RepositoryContext context) : base(context)
        {

        }

        public void AddAppartment(Appartments apartment)
        {
            var check = Context.Appartments.FirstOrDefault(i => i.OwnerId == apartment.OwnerId);
            if (check != null) throw new Exception("One user can only have one apartment");

            Create(apartment);
        }

        public AppartmentDetails GetAppartmentDetails(Guid appartmentId)
        {
            var apartment = Context.Appartments.FirstOrDefault(x => x.Id == appartmentId);
            if (apartment == null) throw new ArgumentNullException("Apartment doesnot exists");

            var avaliabilty = from bg in Context.BookingGuests
                              where bg.HostId == apartment.OwnerId && bg.status == Status.Accepted
                              join guests in Context.Users on bg.GuestId equals guests.Id
                              select new AvaliabilityInfo
                              {
                                  Guest = guests,
                                  From = bg.From,
                                  To = bg.To
                              };
            return new AppartmentDetails() { Apartment = apartment, Avalibilities = avaliabilty };
        }

        public void UpdateAppartment(Appartments appartments) => Update(appartments);
    }
}
