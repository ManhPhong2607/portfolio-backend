using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.BlogPosts.Commands.IncrementViewCount
{
    public record IncrementViewCountCommand(string Slug) : IRequest;
}
