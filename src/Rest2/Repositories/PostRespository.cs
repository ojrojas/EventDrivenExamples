using EventDrivenDesign.Rest2.Data;
using EventDrivenDesign.Rest2.Interfaces;
using EventDrivenDesign.Rest2.Models;
using Microsoft.EntityFrameworkCore;

namespace EventDrivenDesign.Rest2.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly Rest2DbContext _context;

        public PostRepository()
        {
            _context = new Rest2DbContext();
        }

        public async Task<Post> CreatePost(Post post, CancellationToken cancellationToken)
        {
            await _context.Posts.AddAsync(post);
            return await SaveAsync(cancellationToken) ? post : default;
        }

        public async Task<bool> DeletePost(Guid Id, CancellationToken cancellationToken)
        {
            var user = _context.Posts.FirstOrDefault(x => x.Id == Id);
            _context.Posts.Remove(user);
            return await SaveAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Post>> GetAllPost(CancellationToken cancellationToken)
        {
            return await _context.Posts.ToListAsync(cancellationToken);
        }

        public async Task<Post> GetByIdPost(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.Posts.FindAsync(new object[] { Id }, cancellationToken);
        }

        public async Task<Post> UpdatePost(Guid Id, Post post, CancellationToken cancellationToken)
        {
            var postUpdate = await GetByIdPost(Id, cancellationToken);
            postUpdate.Content = post.Content;
            postUpdate.Title = post.Title;
            post.UserId = post.UserId;
            _context.Update(postUpdate);
            return await SaveAsync(cancellationToken) ? post : null;
        }

        private async Task<bool> SaveAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}