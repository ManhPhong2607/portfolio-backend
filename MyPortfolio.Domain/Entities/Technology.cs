using MyPortfolio.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Common;
namespace MyPortfolio.Domain.Entities
{
    public class Technology : BaseEntity
    {
        public string Name { get; private set; }
        public string? IconUrl { get; private set; }

        public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
        private Technology() { }

        public static Technology Create(string name, string? iconUrl = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Tên technology không được rỗng.");

            return new Technology { Name = name.Trim(), IconUrl = iconUrl };
        }

        public void Update(string name, string? iconUrl)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Tên technology không được rỗng.");

            Name = name.Trim();
            IconUrl = iconUrl;
            SetUpdated();
        }
    }
}
