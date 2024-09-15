namespace BlogAPI.DTOs.CommentDTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public DateTime PostCreateDate { get; set; }
        public string PostUserName { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
