using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.BlogPosts.Commands.UpdateBlog
{
    public class UpdateBlogValidator : AbstractValidator<UpdateBlogCommand>
    {
        public UpdateBlogValidator() 
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id không được rỗng.");
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Tiêu đề không được rỗng.")
                .MaximumLength(200).WithMessage("Tiêu đề tối đa 200 ký tự.");
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Nội dung không được rỗng.");
        }
    }
}
