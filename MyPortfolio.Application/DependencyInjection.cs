using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using FluentValidation;
using MyPortfolio.Application.Common.Behaviors;

namespace MyPortfolio.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //MediatR - tự scan toàn bộ handlers trong assembly
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
            //fluentValidation- tự scan toàn bộ validators
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly); 
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));  
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>)); 
            return services;
        }
    }
}
