using MediatR;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Common;
namespace MyPortfolio.Application.Features.Skills.Commands.UpdateSkill
{
    public class UpdateSkillHandler : IRequestHandler<UpdateSkillCommand>
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSkillHandler(ISkillRepository skillRepository, IUnitOfWork unitOfWork)
        {
            _skillRepository = skillRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateSkillCommand request, CancellationToken ct)
        {
            var skill = await _skillRepository.GetByIdAsync(request.Id, ct)
                ?? throw new NotFoundException($"Skill{request.Id} không tồn tại.");

            skill.Update(request.Name, request.Category, request.ProficiencyLevel, request.IconUrl);
            await _skillRepository.UpdateAsync(skill, ct);
            await _unitOfWork.SaveChangesAsync(ct);
        }
    }
}
