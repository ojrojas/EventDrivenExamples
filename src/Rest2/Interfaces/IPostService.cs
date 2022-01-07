
using EventDrivenDesign.Rest2.Dtos;

namespace EventDrivenDesign.Rest2.Interfaces
{
    public interface IPostService
    {
        Task<PostDto> CreatePost(PostDto post, CancellationToken token);
        Task<PostDto> UpdatePost(Guid Id, PostDto post, CancellationToken token);
        Task<bool> DeletePost(Guid Id, CancellationToken token);
        Task<PostDto> GetByIdPost(Guid Id, CancellationToken token);
        Task<IReadOnlyList<PostDto>> GetAllPost(CancellationToken token);
    }
}