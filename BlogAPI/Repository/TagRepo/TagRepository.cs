using BlogAPI.Data;
using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Repository.TagRepo
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _context;
        public TagRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Tag>> GetAllAsync()
        {
            return await _context.Tags.Include(t => t.PostTags).ToListAsync();
        }

        public async Task<Tag?> GetByIdAsync(int id)
        {
            return await _context.Tags.Include(t => t.PostTags).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Tag> CreateAsync(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
            _context.SaveChanges();

            return tag;
        }

        public Tag? Update(Tag tag)
        {
            _context.Tags.Update(tag);
            _context.SaveChanges();

            return tag;
        }

        public Tag? Delete(Tag tag)
        {
            _context.Tags.Remove(tag);
            _context.SaveChanges();

            return tag;
        }
    }
}
