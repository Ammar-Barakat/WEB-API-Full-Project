using BlogAPI.Data;
using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Repository.CommentRepo
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.Include(c => c.User).Include(c => c.Post).ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.Include(c => c.User).Include(c => c.Post).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            _context.SaveChanges();

            return comment;
        }

        public Comment? Update(Comment comment)
        {
            _context.Comments.Update(comment);
            _context.SaveChanges();

            return comment;
        }

        public Comment? Delete(Comment comment)
        {
            _context.Comments.Remove(comment);
            _context.SaveChanges();

            return comment;
        }
    }
}
