using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Interfaces.Repositories;
using MyPortfolio.Domain.Common;
namespace MyPortfolio.Application.Features.Experiences.Commands.ReorderExperience
{
    public class ReorderExperienceHandler : IRequestHandler<ReorderExperienceCommand>   
    {
        private readonly IExperienceRepository _experienceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReorderExperienceHandler(IExperienceRepository experienceRepository, IUnitOfWork unitOfWork)
        {
            _experienceRepository = experienceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ReorderExperienceCommand request, CancellationToken ct)
        {
            var experiences = await _experienceRepository.GetAllAsync(ct);

            for (var i = 0; i < request.OrderIds.Count; i++)
            {
                var exp = experiences.FirstOrDefault(e => e.Id == request.OrderIds[i])
                    ?? throw new NotFoundException($"Experience {request.OrderIds[i]} không tồn tại");

                exp.SetOrder(i);
                await _experienceRepository.UpdateAsync(exp, ct);
            }

            await _unitOfWork.SaveChangesAsync(ct);
        }
    }
}
