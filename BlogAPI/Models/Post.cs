namespace BlogAPI.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public List<Comment> Comments { get; set; }
        public List<PostTag> PostTags { get; set; } = new List<PostTag>();
    }
}
