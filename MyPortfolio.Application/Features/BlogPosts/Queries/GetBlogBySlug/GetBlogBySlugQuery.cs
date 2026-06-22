using MediatR;
using MyPortfolio.Application.Features.BlogPosts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.BlogPosts.Queries.GetBlogBySlug
{
    public record GetBlogBySlugQuery(string Slug) : IRequest<BlogPostDetailDto>;
 
}
