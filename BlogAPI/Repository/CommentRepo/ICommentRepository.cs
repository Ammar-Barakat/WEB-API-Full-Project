using BlogAPI.Models;

namespace BlogAPI.Repository.CommentRepo
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment comment);
        Comment? Update(Comment comment);
        Comment? Delete(Comment comment);
    }
}
