using MediatR;
using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Experiences.Commands.CreateExperience
{
    public class CreateExperienceHandler : IRequestHandler<CreateExperienceCommand, Guid>
    {
        private readonly IExperienceRepository _experienceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateExperienceHandler(IExperienceRepository experienceRepository, IUnitOfWork unitOfWork)
        {
            _experienceRepository = experienceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateExperienceCommand request, CancellationToken ct)
        {
            var experience = Experience.Create(
                request.CompanyName,
                request.Position,
                request.EmploymentType,
                request.StartDate,
                request.EndDate,
                request.IsCurrent,
                request.Location,
                request.Description);

            //experience.Update(
            //    request.CompanyName,
            //    request.Position,
            //    request.EmploymentType,
            //    request.StartDate,
            //    request.EndDate,
            //    request.IsCurrent,
            //    request.Location,
            //    request.Description);

            await _experienceRepository.AddAsync(experience, ct);
            await _unitOfWork.SaveChangesAsync(ct);
            return experience.Id;
        }
    }
}
