using MediatR;
using MyPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Domain.Common;
namespace MyPortfolio.Application.Features.Skills.Commands.DeleteSkill
{
    public class DeleteSkillHandler : IRequestHandler<DeleteSkillCommand>   
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSkillHandler(ISkillRepository skillRepository, IUnitOfWork unitOfWork)
        {
            _skillRepository = skillRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteSkillCommand request, CancellationToken ct)
        {
            var skill = await _skillRepository.GetByIdAsync(request.Id, ct)
                ?? throw new NotFoundException($"skill{request.Id} không tồn tại.");
            await _skillRepository.DeleteAsync(skill, ct);
            await _unitOfWork.SaveChangesAsync(ct); 
        }
    }
}
