using Entities.DataTransferObjects;
using Entities.Models;

namespace Contracts
{
    public interface IApartmentRepository : IRepositoryBase<Apartments>
    {
        Apartments? GetUserApartment(Guid? userId);
        void AddApartment(Apartments apartment);
        void UpdateApartment(Guid? ownerId, ApartmentBase update);
        object GetApartmentDetails(Guid? apartmentId);
    }
}
