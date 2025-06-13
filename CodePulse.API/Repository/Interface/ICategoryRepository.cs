using CodePulse.API.Models.Domain;

namespace CodePulse.API.Repository.Interface
{
    public interface ICategoryRepository
    {
        public Task<Category> CreateAsync(Category category);
        public Task<IEnumerable<Category>> GetAllAsync();
        public Task<Category?> GetById(Guid id);
        public Task<Category?> UpdateAsync(Category category);

        public Task<Category?> DeleteAsync(Guid id);
    }
}
