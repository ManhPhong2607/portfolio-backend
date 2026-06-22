using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyPortfolio.Domain.Common;
using MyPortfolio.Domain.Interfaces.Repositories;
namespace MyPortfolio.Application.Features.Experiences.Commands.UpdateExperience
{
    public class UpdateExperienceHandler :IRequestHandler<UpdateExperienceCommand>  
    {
        private readonly IExperienceRepository _experienceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateExperienceHandler(IExperienceRepository experienceRepository, IUnitOfWork unitOfWork)
        {
            _experienceRepository = experienceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateExperienceCommand request, CancellationToken ct)
        {
            var experience = await _experienceRepository.GetByIdAsync(request.Id, ct)
                ?? throw new NotFoundException($"Experience {request.Id} không tồn tại.");

            experience.Update(
                request.CompanyName,
                request.Position,              
                request.EmploymentType,
                request.StartDate,
                request.EndDate,
                request.IsCurrent,
                request.Location,
                 request.Description
             );

            await _experienceRepository.UpdateAsync(experience, ct);
            await _unitOfWork.SaveChangesAsync(ct);
        }
    }
}
