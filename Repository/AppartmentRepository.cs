using Contracts;
using Entities;
using Entities.DataTransferObjects;
using Entities.Helper;
using Entities.Models;

namespace Repository
{
    internal class ApartmentRepository :RepositoryBase<Apartments>, IApartmentRepository
    {
        public ApartmentRepository(RepositoryContext context) : base(context)
        {

        }

        public void AddApartment(Apartments apartment)
        {
            var check = Context.Apartments.FirstOrDefault(i => i.OwnerId == apartment.OwnerId);
            if (check != null) throw new Exception("One user can only have one apartment");

            Create(apartment);
        }

        public object GetApartmentDetails(Guid? apartmentId)
        {
            var apartment = Context.Apartments.FirstOrDefault(x => x.Id == apartmentId);
            if (apartment == null) throw new ArgumentNullException("Apartment doesnot exists");

            var avaliabilty = from bg in Context.BookingGuests
                              where bg.HostId == apartment.OwnerId && bg.status == Status.Accepted
                              join guests in Context.Users on bg.GuestId equals guests.Id
                              select new ReturnGuestsDto
                              {
                                  Id = guests.Id,
                                  From = bg.From,
                                  To = bg.To,
                                  Email = guests.Email,
                                  Description = guests.Description,
                                  LastName = guests.LastName,
                                  FirstName = guests.FirstName,
                                  Image = guests.Image,
                                  Status = bg.status.ToString()
                              };

            return new { Apartment = apartment, Avalibilities = avaliabilty };
        }

        public Apartments? GetUserApartment(Guid? userId)
        {
            if (userId == null) throw new ArgumentNullException("userId");
            return Context.Apartments.FirstOrDefault(i => i.OwnerId == userId);
        }

        public void UpdateApartment(Guid? ownerId, ApartmentBase update)
        {
            var apartment = Context.Apartments.FirstOrDefault(i => i.OwnerId == ownerId);
            if (apartment != null)
            {
                apartment.Address = update.Address ?? apartment.Address;

                apartment.DistanceFromCenter = update.DistanceFromCenter == default ? apartment.DistanceFromCenter : update.DistanceFromCenter;

                apartment.NumOfBeds = update.NumOfBeds == default ? 1 : update.NumOfBeds;

                apartment.City = update.City ?? apartment.City;

                apartment.Description = update.Description ?? apartment.Description;

                apartment.Image = update.Image ?? apartment.Image;


                apartment.Gym = update.Gym ?? false;
                apartment.Parking = update.Parking ?? false;
                apartment.Pool = update.Pool ?? false;
                apartment.Wifi = update.Wifi ?? false;
            }
        }
    }
}
