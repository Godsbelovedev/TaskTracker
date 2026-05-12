using System.Runtime.CompilerServices;
using TaskTracker.Model.Entities;

namespace TaskTracker.Infrastructure.Interfaces
{
    public interface ICategoryRepositoryInterface
    {
         bool AddCategory(Category category);
         bool DeleteCategory(Guid id);
         bool UpdateCategory(Guid id, string name, string description);
         Category? GetCategoryById(Guid id);
         List<Category> GetByDetail(string searchItem);
         List<Category> GetAllCategories();
         //Category GetCurrentCategory();
    }
}