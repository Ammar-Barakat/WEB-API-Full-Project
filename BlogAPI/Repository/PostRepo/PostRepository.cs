using BlogAPI.Data;
using BlogAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Repository.PostRepo
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;
        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetAllAsync()
        {
            return await _context.Posts.Include(p => p.User)
                .Include(p => p.Comments)
                    .ThenInclude(pc => pc.User)
                .Include(p => p.PostTags)
                    .ThenInclude(pt => pt.Tag)
                .ToListAsync();
        }

        public async Task<Post?> GetByIdAsync(int id)
        {
            return await _context.Posts.Include(p => p.User)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.User)
                .Include(p => p.PostTags)
                    .ThenInclude(pt => pt.Tag)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Post> CreateAsync(Post post)
        {
            await _context.Posts.AddAsync(post);
            _context.SaveChanges();

            return post;
        }

        public Post? Update(Post post)
        {
            _context.Posts.Update(post);
            _context.SaveChanges();

            return post;
        }

        public Post? Delete(Post post)
        {
            _context.Posts.Remove(post);
            _context.SaveChanges();

            return post;
        }

        public async Task<Post?> AssignPostTag(Post post, List<int> tagIds)
        {
            foreach (var tagId in tagIds)
            {
                var tag = await _context.Tags.FindAsync(tagId);

                if (tag == null)
                    return null;

                await _context.PostTags.AddAsync(new PostTag { PostId = post.Id, TagId = tagId });
                _context.SaveChanges();
            }

            return post;
        }
    }
}
