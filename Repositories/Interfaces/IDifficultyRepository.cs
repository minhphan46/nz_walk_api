using UdemyProject1.Models.Domain;

namespace UdemyProject1.Repositories.Interfaces
{
    public interface IDifficultyRepository
    {
        Task<List<Difficulty>> GetAllAsync();

        Task<Difficulty> GetByIdAsync(Guid id);

        Task<Difficulty> CreateAsync(Difficulty difficulty);

        Task<Difficulty> UpdateAsync(Guid id, Difficulty difficulty);

        Task<Difficulty> DeleteAsync(Guid id);
    }
}
