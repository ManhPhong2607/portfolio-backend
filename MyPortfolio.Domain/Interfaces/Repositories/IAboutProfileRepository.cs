using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Entities;

namespace MyPortfolio.Domain.Interfaces.Repositories
{
    public interface IAboutProfileRepository
    {
        Task<AboutProfile> GetAsync(CancellationToken ct = default);
        Task UpdateAsync(AboutProfile profile, CancellationToken ct = default);
    }
}
