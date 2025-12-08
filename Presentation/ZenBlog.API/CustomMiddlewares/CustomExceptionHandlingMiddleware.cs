using FluentValidation;
using ZenBlog.Application.Base;

namespace ZenBlog.API.CustomMiddlewares
{
    public class CustomExceptionHandlingMiddleware(RequestDelegate next)
    {

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var response = new BaseResult<object>()
                {
                    Errors=ex.Errors.GroupBy(x=>x.PropertyName)
                    .Select(x=> new Error
                    {
                        PropertyName = x.Key,
                        ErrorMessage = x.Select(x=>x.ErrorMessage).FirstOrDefault()
                    }).ToList()
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }

    }
}
