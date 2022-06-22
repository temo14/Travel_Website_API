using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        IAppartmentRepository Appartment { get; }
        IActions Actions { get; }
        IJwtUtils JWT { get; }
        void Save();
    }
}
