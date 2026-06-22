using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Enums;

namespace MyPortfolio.Domain.Entities
{
    public class Skill : BaseEntity
    {
        public string Name { get; private set; }
        public SkillCategory Category { get; private set; }
        public int ProficiencyLevel { get; private set; }
        public string? IconUrl { get; private set; }
        public int DisplayOrder { get; private set; }

        private Skill() { }

        public static Skill Create(string name, SkillCategory category, int proficiencyLevel, string? IconUrl)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Tên kỹ năng không được rỗng.");

            if (proficiencyLevel < 1 || proficiencyLevel > 5)
                throw new DomainException("Mức độ thành thạo phải từ 1 đến 5.");

            return new Skill
            {
                Name = name.Trim(),
                Category = category,
                ProficiencyLevel = proficiencyLevel,
                IconUrl = IconUrl?.Trim(),
               
            };
        }

        public void Update(string name, SkillCategory category, int proficiencyLevel, string? IconUrl)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Tên skill không được rỗng.");

            if (proficiencyLevel < 1 || proficiencyLevel > 5)
                throw new DomainException("ProficiencyLevel phải từ 1 đến 5.");

            Name = name.Trim();
;           Category = category;
            ProficiencyLevel = proficiencyLevel;
            IconUrl = IconUrl?.Trim();
            
            SetUpdated();
        }

        public void SetOrder(int order)
        {
            DisplayOrder = order;
            SetUpdated();
        }
    }
}
