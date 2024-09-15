namespace BlogAPI.DTOs.CommentDTOs
{
    public class CreateCommentDTO
    {
        public string Content { get; set; }
        public int PostId { get; set; }
    }
}
