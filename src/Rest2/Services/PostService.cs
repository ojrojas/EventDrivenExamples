using EventDrivenDesign.Rest2.Dtos;
using EventDrivenDesign.Rest2.Interfaces;

namespace EventDrivenDesign.Rest2.Services
{
    public class PostService : IPostService
    {
        public Task<PostDto> CreatePost(PostDto post, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePost(Guid Id, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<PostDto>> GetAllPost(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<PostDto> GetByIdPost(Guid Id, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<PostDto> UpdatePost(Guid Id, PostDto post, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}