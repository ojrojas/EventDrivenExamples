using EventDrivenDesign.Rest2.Models;

namespace EventDrivenDesign.Rest2.Interfaces
{
    public interface IPostRepository
    {
        Task<Post> CreatePost(Post post, CancellationToken cancellationToken);
        Task<Post> UpdatePost(Guid Id, Post post, CancellationToken cancellationToken);
        Task<bool> DeletePost(Guid Id, CancellationToken cancellationToken);
        Task<Post> GetByIdPost(Guid Id, CancellationToken cancellationToken);
        Task<IReadOnlyList<Post>> GetAllPost(CancellationToken cancellationToken);
    }
}