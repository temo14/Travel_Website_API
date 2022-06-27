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
        void AddApartment(Apartments apartments);
        void UpdateApartment(Apartments user);
        ApartmentDetails GetApartmentDetails(Guid apartmentId);
    }
}
