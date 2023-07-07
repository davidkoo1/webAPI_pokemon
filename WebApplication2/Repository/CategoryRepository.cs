using WebApplication2.Data;
using WebApplication2.Interfaces;
using WebApplication2.Models;

namespace WebApplication2.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _dataContext;
        public CategoryRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public bool CategoryExists(int id) => _dataContext.Categories.Any(c => c.Id == id);

        public bool CreateCategory(Category category)
        {
            _dataContext.Add(category);

            return Save();
        }

        public ICollection<Category> GetCategories() => _dataContext.Categories.OrderBy(c => c.Id).ToList();

        public Category GetCategory(int id) => _dataContext.Categories.Where(c => c.Id == id).FirstOrDefault();

        public ICollection<Pokemon> GetPokemonByCategory(int categoryId) => _dataContext.PokemonCategories
            .Where(pc => pc.CategoryId == categoryId)
            .Select(c => c.Pokemon)
            .ToList();

        public bool Save() => _dataContext.SaveChanges() > 0 ? true : false;

        public bool UpdateCategory(Category category)
        {
            _dataContext.Update(category);

            return Save();
        }
    }
}
