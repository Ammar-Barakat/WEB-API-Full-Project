namespace BlogAPI.DTOs.PostDTOs
{
    public class CreatePostDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public List<int> tagIds { get; set; }
    }
}
