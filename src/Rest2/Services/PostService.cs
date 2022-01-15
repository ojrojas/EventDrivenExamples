using AutoMapper;
using EventDrivenDesign.Rest2.Dtos;
using EventDrivenDesign.Rest2.Interfaces;
using EventDrivenDesign.Rest2.Models;

namespace EventDrivenDesign.Rest2.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ILogger<PostService> _logger;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, ILogger<PostService> logger, IMapper mapper)
        {
            _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PostDto> CreatePost(PostDto PostDto, CancellationToken cancellationToken)
        {
            var post = _mapper.Map<Post>(PostDto);
            _logger.LogInformation("creating user into api rest1");
            var result = _mapper.Map<PostDto>(await _postRepository.CreatePost(post, cancellationToken));
            _logger.LogInformation("sending queue info user created");

            return result;
        }

        public async Task<bool> DeletePost(Guid Id, CancellationToken cancellationToken)
        {
            return await _postRepository.DeletePost(Id, cancellationToken);
        }

        public async Task<IReadOnlyList<PostDto>> GetAllPost(CancellationToken cancellationToken)
        {
            return _mapper.Map<IReadOnlyList<PostDto>>(await _postRepository.GetAllPost(cancellationToken));
        }

        public async Task<PostDto> GetByIdPost(Guid Id, CancellationToken cancellationToken)
        {
           return  _mapper.Map<PostDto>(await _postRepository.GetByIdPost(Id, cancellationToken));
        }

        public async Task<PostDto> UpdatePost(Guid Id, PostDto postDto, CancellationToken cancellationToken)
        {
              var post = _mapper.Map<Post>(postDto);
            return  _mapper.Map<PostDto>(await _postRepository.UpdatePost(Id, post, cancellationToken));
        }
    }
}