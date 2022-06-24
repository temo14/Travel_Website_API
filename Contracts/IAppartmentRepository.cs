 using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAppartmentRepository : IRepositoryBase<Appartments>
    {
        void AddAppartment(Appartments appartments);
        void UpdateAppartment(Appartments user);
        AppartmentDetails GetAppartmentDetails(Guid appartmentId);
    }
}
