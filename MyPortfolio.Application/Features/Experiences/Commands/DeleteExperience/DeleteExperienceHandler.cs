using MediatR;
using MyPortfolio.Domain.Interfaces.Repositories;
using MyPortfolio.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Experiences.Commands.DeleteExperience
{
    public class DeleteExperienceHandler : IRequestHandler<DeleteExperienceCommand>
    {
        private readonly IExperienceRepository _experienceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteExperienceHandler(IExperienceRepository experienceRepository, IUnitOfWork unitOfWork)
        {
            _experienceRepository = experienceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteExperienceCommand request, CancellationToken ct)
        {
            var experience = await _experienceRepository.GetByIdAsync(request.Id, ct)
                ?? throw new NotFoundException($"Experience{request.Id} không tồn tại.");

            await _experienceRepository.DeleteAsync(experience, ct);
            await _unitOfWork.SaveChangesAsync(ct);
        }
    }
}
