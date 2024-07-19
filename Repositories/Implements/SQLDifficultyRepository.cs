using UdemyProject1.Models.Domain;
using UdemyProject1.Repositories.Interfaces;

namespace UdemyProject1.Repositories.Implements
{
    public class SQLDifficultyRepository : IDifficultyRepository
    {
        public Task<Difficulty> CreateAsync(Difficulty difficulty)
        {
            throw new NotImplementedException();
        }

        public Task<Difficulty> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Difficulty>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Difficulty> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Difficulty> UpdateAsync(Guid id, Difficulty difficulty)
        {
            throw new NotImplementedException();
        }
    }
}
