using MediatR;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Common;
namespace MyPortfolio.Application.Features.Skills.Commands.ReorderSkills
{
    public class ReorderSkillsHandler : IRequestHandler<ReorderSkillsCommand>
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReorderSkillsHandler(ISkillRepository skillRepository, IUnitOfWork unitOfWork)
        {
            _skillRepository = skillRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ReorderSkillsCommand request, CancellationToken ct)
        {
            var skills = await _skillRepository.GetAllAsync(ct);

            for (int i = 0; i < request.OrderedIds.Count; i++)
            {
                var skill = skills.FirstOrDefault(s => s.Id == request.OrderedIds[i])
                    ?? throw new DomainException($"Skill {request.OrderedIds[i]} không tồn tại.");
                skill.SetOrder(i);
                await _skillRepository.UpdateAsync(skill, ct);
            }

            await _unitOfWork.SaveChangesAsync(ct);
        }
    }
}
