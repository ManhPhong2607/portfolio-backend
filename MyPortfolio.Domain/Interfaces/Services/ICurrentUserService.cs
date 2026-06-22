using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Domain.Interfaces.Services
{
    public interface ICurrentUserService
    {
        Guid UserId { get;}
        string Role { get; }
        bool IsAuthenticated { get; }
    }

}
