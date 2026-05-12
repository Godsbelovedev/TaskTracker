using TaskTracker.Model.Entities;

namespace TaskTracker.Application.Interfaces
{
    public interface ICategoryServiceInterface
    {
         
         void AddCategory(Category category);
         void DeleteCategory(Guid id);
         void UpdateCategory(Guid id, string name, string description);
         void SearchCategory(string searchTerm);
         void GetCategoryById(Guid id);
         void GetAllCategories();
    }
}
