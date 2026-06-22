using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Tags.Commands.DeleteTag
{
    public record DeleteTagCommand(Guid Id 
       // bool Force = false  // true = xóa dù đang dùng
        ): IRequest;

    //public record DeleteTagResult(
    //    bool Deleted, bool WasInUse, string? Message);
    
}
