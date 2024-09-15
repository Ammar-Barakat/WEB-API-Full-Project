namespace BlogAPI.DTOs.CommentDTOs
{
    public class CommentPostDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
