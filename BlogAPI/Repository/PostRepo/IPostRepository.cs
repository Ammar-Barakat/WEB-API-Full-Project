using BlogAPI.Models;

namespace BlogAPI.Repository.PostRepo
{
    public interface IPostRepository
    {
        Task<List<Post>> GetAllAsync();
        Task<Post?> GetByIdAsync(int id);
        Task<Post> CreateAsync(Post post);
        Post? Update(Post post);
        Post? Delete(Post post);
        Task<Post?> AssignPostTag(Post post, List<int> tagIds);
    }
}
