using Contracts;
using Entities;
using Entities.DataTransferObjects;
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

        public void AddApartment(Apartments apartment)
        {
            var check = Context.Apartments.FirstOrDefault(i => i.OwnerId == apartment.OwnerId);
            if (check != null) throw new Exception("One user can only have one apartment");

            Create(apartment);
        }

        public ApartmentDetails GetApartmentDetails(Guid apartmentId)
        {
            var apartment = Context.Apartments.FirstOrDefault(x => x.Id == apartmentId);
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
            return new ApartmentDetails() { Apartment = apartment, Avalibilities = avaliabilty };
        }

        public Apartments? GetUserApartment(Guid userId)
        {
            return Context.Apartments.FirstOrDefault(i => i.OwnerId == userId);
        }

        public void UpdateApartment(Guid? ownerId, ApartmentBase update)
        {
            var apartment = Context.Apartments.FirstOrDefault(i => i.OwnerId == ownerId);
            if (apartment != null)
            {
                apartment.Address = update.Address == null || update.Address == default ? apartment.Address : update.Address;

                apartment.DistanceFromCenter = update.DistanceFromCenter == default ? apartment.DistanceFromCenter : update.DistanceFromCenter;

                apartment.NumOfBeds = update.NumOfBeds == default ? apartment.NumOfBeds : update.NumOfBeds;

                apartment.City = update.City == null ? apartment.City : update.City;

                apartment.Description = update.Description == null ? apartment.Description : update.Description;

                apartment.Image = update.Image == null ? apartment.Image : update.Image;


                apartment.Gym = update.Gym is not null ? apartment.Gym : update.Gym;
                apartment.Parking = update.Parking is not null ? apartment.Parking : update.Parking;
                apartment.Pool = update.Pool is not null ? apartment.Pool : update.Pool;
                apartment.Wifi = update.Wifi is not null ? apartment.Wifi : update.Wifi;
            }
        }
    }
}
