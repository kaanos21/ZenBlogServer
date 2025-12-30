using MediatR;

namespace ZenBlog.API.Endpoints
{
    public static class CommentEndpoints
    {
        public static void RegisterCommentEndpoints(this IEndpointRouteBuilder app)
        {
            var comments=app.MapGroup("/api/comments").WithTags("Comments");

            comments.MapGet("/api/comments", 
                async (IMediator mediator) =>
            {
                var query = new ZenBlog.Application.Features.Comments.Queries.GetCommentsQuery();
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });
            
        }
    }
}
