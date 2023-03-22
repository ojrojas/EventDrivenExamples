namespace EventDrivenDesign.Rest2.Interfaces
{
    public interface IPostService
    {
        Task<PostDto> CreatePost(PostDto post, CancellationToken cancellationToken);
        Task<PostDto> UpdatePost(Guid Id, PostDto post, CancellationToken cancellationToken);
        Task<bool> DeletePost(Guid Id, CancellationToken cancellationToken);
        Task<PostDto> GetByIdPost(Guid Id, CancellationToken cancellationToken);
        Task<IReadOnlyList<PostDto>> GetAllPost(CancellationToken cancellationToken);
    }
}