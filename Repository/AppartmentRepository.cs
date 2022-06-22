using Contracts;
using Entities;
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

        public void AddAppartment(Appartments appartments) => Create(appartments);

        public void UpdateAppartment(Appartments appartments) => Update(appartments);
    }
}
