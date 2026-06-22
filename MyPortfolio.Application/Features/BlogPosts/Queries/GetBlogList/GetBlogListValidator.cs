using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.BlogPosts.Queries.GetBlogList
{
    public class GetBlogListValidator : AbstractValidator<GetBlogListQuery>
    {
        public GetBlogListValidator() 
        {
            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(1).WithMessage("Page >= 1");

            RuleFor(x => x.Limit)
                .InclusiveBetween(1, 50).WithMessage("Limit phải từ 1 đến 50.");
        }
    }
}
