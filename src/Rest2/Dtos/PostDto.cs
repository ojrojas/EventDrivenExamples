namespace EventDrivenDesign.Rest2.Dtos
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid UserId {get;set;}
        public UserDto User { get; set; }
    }
}