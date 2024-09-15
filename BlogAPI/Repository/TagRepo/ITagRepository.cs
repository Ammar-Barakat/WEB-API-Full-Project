using BlogAPI.Models;

namespace BlogAPI.Repository.TagRepo
{
    public interface ITagRepository
    {
        Task<List<Tag>> GetAllAsync();
        Task<Tag?> GetByIdAsync(int id);
        Task<Tag> CreateAsync(Tag tag);
        Tag? Update(Tag tag);
        Tag? Delete(Tag tag);
    }
}
