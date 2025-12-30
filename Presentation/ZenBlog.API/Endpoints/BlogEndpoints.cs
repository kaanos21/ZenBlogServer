using MediatR;
using ZenBlog.Application.Features.Blogs.Commands;
using ZenBlog.Application.Features.Blogs.Queries;

namespace ZenBlog.API.Endpoints
{
    public static class BlogEndpoints
    {
        public static void RegisterBlogEndpoints(this IEndpointRouteBuilder app)
        {
            var blogs = app.MapGroup("/blogs").WithTags("Blogs");

            blogs.MapGet("",
                async (IMediator mediator) =>
                {
                    var response = await mediator.Send(new ZenBlog.Application.Features.Blogs.Queries.GetBlogsQuery());
                    return response.IsSuccess ? Results.Ok(response) : Results.BadRequest(response);
                });

            blogs.MapPost(string.Empty,
                async (CreateBlogCommand command, IMediator mediator) =>
                {
                    var response = await mediator.Send(command);
                    return response.IsSuccess ? Results.Ok(response) : Results.BadRequest(response);
                });

            blogs.MapGet("/{id:guid}",
                async (Guid id, IMediator mediator) =>
                {
                    var response = await mediator.Send(new GetBlogByIdQuery(id));
                    return response.IsSuccess ? Results.Ok(response) : Results.NotFound(response);
                });

            blogs.MapPut("/{id:guid}",
                async (Guid id, UpdateBlogCommand command, IMediator mediator) =>
                {
                    if (id != command.Id)
                    {
                        return Results.BadRequest("ID in URL does not match ID in body.");
                    }
                    var response = await mediator.Send(command);
                    return response.IsSuccess ? Results.Ok(response) : Results.BadRequest(response);
                });

            blogs.MapDelete("/{id:guid}",
                async (Guid id, IMediator mediator) =>
                {
                    var response = await mediator.Send(new RemoveBlogCommand(id));
                    return response.IsSuccess ? Results.Ok(response) : Results.BadRequest(response);
                });

            blogs.MapGet("/byCategoryId{categoryId}",
                async (Guid categoryId, IMediator mediator) =>
                {
                    var response=await mediator.Send(new GetBlogsByCategoryIdQuery(categoryId));
                    return response.IsSuccess ? Results.Ok(response) : Results.BadRequest(response);
                });
        }
    }
}
