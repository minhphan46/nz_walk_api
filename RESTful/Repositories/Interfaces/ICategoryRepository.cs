using UdemyProject1.Entities;

namespace UdemyProject1.RESTful.Repositories.Interfaces
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
