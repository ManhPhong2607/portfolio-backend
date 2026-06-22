using MyPortfolio.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Enums;
namespace MyPortfolio.Domain.Entities
{
    public class Experience : BaseEntity
    {
        public string CompanyName { get; private set; } = default!;
        public string Position { get; private set; } = default!;
        public string? Location { get; private set; }
        public string? Description { get; private set; }
        public EmploymentType EmploymentType { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public bool IsCurrent { get; private set; }
        public int DisplayOrder { get; private set; }

        private Experience() { }

        public static Experience Create(
            string companyName,
            string position,
            EmploymentType employmentType,
            DateTime startDate,
            DateTime? endDate,
            bool isCurrent,
            string? location,
            string? description
            )
        {
            if (string.IsNullOrWhiteSpace(companyName))
                throw new DomainException("Tên công ty không được rỗng.");

            if (string.IsNullOrWhiteSpace(position))
                throw new DomainException("Vị trí không được rỗng.");

            return new Experience
            {
                CompanyName = companyName.Trim(),
                Position = position.Trim(),
                EmploymentType = employmentType,
                StartDate = DateTime.SpecifyKind(startDate.Date, DateTimeKind.Utc),
                EndDate = isCurrent || !endDate.HasValue
                   ? null
                   : DateTime.SpecifyKind(endDate.Value.Date, DateTimeKind.Utc),
                IsCurrent = isCurrent,
                Location = location?.Trim(),
                Description = description?.Trim()
            };
        }

        public void Update(string companyName, string position, EmploymentType employmentType,
            DateTime startDate, DateTime? endDate, bool isCurrent, string? location, string? description)
        {
            if (string.IsNullOrWhiteSpace(companyName))
                throw new DomainException("Tên công ty không được rỗng.");

            if (!isCurrent && endDate is null)
                throw new DomainException("EndDate bắt buộc nếu không phải công việc hiện tại.");

            if (endDate.HasValue && endDate.Value < startDate)
                throw new DomainException("EndDate phải sau StartDate.");

            CompanyName = companyName.Trim();
            Position = position.Trim();
            EmploymentType = employmentType;
            StartDate = DateTime.SpecifyKind(startDate.Date, DateTimeKind.Utc);
            EndDate = isCurrent ? null : (endDate.HasValue ? DateTime.SpecifyKind(endDate.Value.Date, DateTimeKind.Utc) : null);
            //StartDate = startDate.ToUniversalTime();
            //EndDate = isCurrent ? null : endDate?.ToUniversalTime();
            IsCurrent = isCurrent;
            Location = location;
            Description = description;
            SetUpdated();
        }

        public void SetOrder(int order)
        {
            DisplayOrder = order;
            SetUpdated();
        }
    }

}

