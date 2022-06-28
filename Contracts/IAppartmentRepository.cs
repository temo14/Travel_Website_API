 using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IApartmentRepository : IRepositoryBase<Apartments>
    {
        Apartments? GetUserApartment(Guid userId);
        void AddApartment(Apartments apartments);
        void UpdateApartment(Apartments apartment);
        ApartmentDetails GetApartmentDetails(Guid apartmentId);
    }
}
