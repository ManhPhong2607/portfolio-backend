using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Domain.Common;
using System.Text.Json;
using System.Net;

namespace MyPortfolio.API.Middleware
{
    public class ExceptionHandling(RequestDelegate next, ILogger<ExceptionHandling> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            //try
            //{
            //    await next(context);
            //}
            //catch (Exception ex)
            //{
            //    logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);
            //    await HandleExceptionAsync(context, ex);
            //}

            try
            {
                await next(context);
            }
            catch (NotFoundException ex)
            {
                logger.LogInformation(ex.Message);
                await HandleExceptionAsync(context, ex);
            }
            catch (ValidationException ex)
            {
                logger.LogWarning(ex.Message);
                await HandleExceptionAsync(context, ex);
            }
            catch (DomainException ex)
            {
                logger.LogWarning(ex.Message);
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/problem+json";
            var problem = ex switch
            {
                NotFoundException nfe => new ProblemDetails
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Title = "Không tìm thấy",
                    Detail = nfe.Message
                },

                DomainException de => new ProblemDetails
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Title = "Lỗi nghiệp vụ",
                    Detail = de.Message
                },

                ValidationException ve => new ProblemDetails
                {
                    Status = (int)HttpStatusCode.UnprocessableEntity,
                    Title = "Dữ liệu không hợp lệ",
                    Detail = string.Join("; ", ve.Errors.Select(e => e.ErrorMessage))
                },

                UnauthorizedAccessException => new ProblemDetails
                {
                    Status = (int)HttpStatusCode.Unauthorized,
                    Title = "Không có quyền truy cập"
                },

                _ => new ProblemDetails
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Title = "Lỗi hệ thống",
                    Detail = "Đã xảy ra lỗi, vui lòng thử lại."
                }
            };

            context.Response.StatusCode = problem.Status!.Value;

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(problem,
                    new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
        }
    }
}
