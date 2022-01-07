using EventDrivenDesign.Rest2.Interfaces;
using EventDrivenDesign.Rest2.Models;

namespace EventDrivenDesign.Rest2.Repositories  
{
    public class PostRepository : IPostRepository
    {
        public Task<Post> CreatePost(Post post, CancellationToken cancellationToken)
        {
             throw new NotImplementedException();
        }

        public Task<bool> DeletePost(Guid Id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Post>> GetAllPost(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Post> GetByIdPost(Guid Id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Post> UpdatePost(Guid Id, Post post, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}