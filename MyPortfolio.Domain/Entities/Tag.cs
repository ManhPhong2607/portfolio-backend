using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Domain.Entities
{
    public class Tag : BaseEntity
    {
        public string Name { get; private set; } = default;
        public Slug Slug { get; private set; } = default;
        public virtual ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();

        private Tag() { }

        public static Tag Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Tag không được rỗng");

            return new Tag
            {
                Name = name.Trim(),
                Slug = Slug.Create(name)
            };
        }

        public void Update(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Tag không được rỗng");

            Name = name.Trim();
            Slug = Slug.Create(name);
            SetUpdated();
        }

    }
}
