using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.BlogPosts.Commands.CreateBlog
{
    public class CreateBlogValidator : AbstractValidator<CreateBlogCommand>
    {
        public CreateBlogValidator() 
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Tiêu đề không được rỗng.")
                .MaximumLength(200).WithMessage("tiêu đề tối đa 200 kí tự.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Nội dung không được rỗng.");

            RuleFor(x => x.Excerpt)
                .MaximumLength(500).WithMessage("Excerpt tối đa 500 kí tự.")
                .When(x => x.Excerpt != null);
        }
    }
}
