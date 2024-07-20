using UdemyProject1.Models.Domain;

namespace UdemyProject1.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();

        Task<Category> GetByIdAsync(Guid id);

        Task<Category> CreateAsync(Category category);

        Task<Category> UpdateAsync(Guid id, Category category);

        Task<Category> DeleteAsync(Guid id);
    }
}
