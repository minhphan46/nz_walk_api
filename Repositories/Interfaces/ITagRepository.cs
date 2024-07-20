using UdemyProject1.Models.Domain;

namespace UdemyProject1.Repositories.Interfaces
{
    public interface ITagRepository
    {
        Task<List<Tag>> GetAllAsync();

        Task<Tag> GetByIdAsync(Guid id);

        Task<Tag> CreateAsync(Tag tag);

        Task<Tag> UpdateAsync(Guid id, Tag tag);

        Task<Tag> DeleteAsync(Guid id);
    }
}
