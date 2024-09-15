using BlogAPI.DTOs.CommentDTOs;
using BlogAPI.DTOs.TagDTOs;
using BlogAPI.Models;

namespace BlogAPI.DTOs.PostDTOs
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<CommentPostDTO>? Comments { get; set; }
        public List<TagDTO> Tags { get; set; }
    }
}
