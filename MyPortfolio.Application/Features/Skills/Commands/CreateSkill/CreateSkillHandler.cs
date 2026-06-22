using MediatR;
using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Skills.Commands.CreateSkill
{
    public class CreateSkillHandler :IRequestHandler<CreateSkillCommand, Guid>
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSkillHandler(ISkillRepository skillRepository, IUnitOfWork unitOfWork)
        {
            _skillRepository = skillRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateSkillCommand request, CancellationToken ct)
        {
            //tạo skill
            var skill = Skill.Create(
                request.Name, 
                request.Category,
                request.ProficiencyLevel,
                request.IconUrl);

            //if(request.IconUrl != null)
            //{
            //    skill.Update(request.Name, request.Category, request.ProficiencyLevel, request.IconUrl);
            //}

            await _skillRepository.AddAsync(skill, ct);
            await _unitOfWork.SaveChangesAsync(ct);
            return skill.Id;

        }
    }
}
