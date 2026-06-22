using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Skills.Commands.ReorderSkills
{
    // orderedIds: danh sách Id theo thứ tự mới từ trên xuống
    public record ReorderSkillsCommand(
        List<Guid>OrderedIds) : IRequest;  
}
