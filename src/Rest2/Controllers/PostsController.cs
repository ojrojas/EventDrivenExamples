using EventDrivenDesign.Rest2.Dtos;
using EventDrivenDesign.Rest2.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventDrivenDesign.Rest2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PostDto>>> GetAllPost(
           CancellationToken cancellationToken) => Ok(await _postService.GetAllPost(cancellationToken));

        [HttpPut]
        public async Task<ActionResult<PostDto>> UpdatePost(
                    [FromRoute] Guid Id, [FromBody] PostDto PostDto, CancellationToken cancellationToken) => Ok(
                        await _postService.UpdatePost(Id, PostDto, cancellationToken));

        [HttpPost]
        public async Task<ActionResult<PostDto>> CreatePost(
                  [FromBody] PostDto PostDto, CancellationToken cancellationToken) => Ok(
                      await _postService.CreatePost(PostDto, cancellationToken));

        [HttpGet("{Id}")]
        public async Task<ActionResult<PostDto>> GetByIdPost(
            [FromRoute] Guid Id, CancellationToken cancellationToken) => Ok(
                await _postService.GetByIdPost(Id, cancellationToken));

        [HttpDelete]
        public async Task<ActionResult<UserDto>> DeletePost(
            [FromRoute] Guid Id, CancellationToken cancellationToken) => Ok(
                await _postService.DeletePost(Id, cancellationToken));
    }
}