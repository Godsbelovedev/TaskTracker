using System.Runtime.CompilerServices;
using TaskTracker.Application.Interfaces;
using TaskTracker.Infrastructure.Interfaces;
using TaskTracker.Model.Entities;

namespace TaskTracker.Application.Implementations
{
    public class CategoryService : ICategoryServiceInterface
    {
        private readonly ICategoryRepositoryInterface _repository;
        private readonly ITodoItemsRepositoryInterface _todoRepository;
        public CategoryService()
        {
            _repository = new CategoryRepository();
            _todoRepository = new TodoItemsRepository();
        }
        public void AddCategory(Category category)
        {
            var newCategory =  _repository.AddCategory(category);
            if(newCategory == false)
            {
                Console.WriteLine("create fail");
                return;
            }
            Console.WriteLine("Create successful");
             Console.WriteLine(new string('-', 40));
        }

        public void DeleteCategory(Guid id)
        {
            var categoryToDelete = _repository.DeleteCategory(id);
            if(categoryToDelete == false)
            {
                Console.WriteLine("Delete fail");
                return;
            }
            var listOftasks = _todoRepository.GetAllToDoItems();
            if(listOftasks == null || !listOftasks.Any())
            {
                Console.WriteLine("no existing task");
                return;
            }
            Category? category = null;
            foreach(var task in listOftasks)
            {
                if(task.CategoryId == id)
                {
                    _todoRepository.Update(task.Id, task.TaskDescription ?? "no description", 
                                        task.Status, task.TaskName, category);
                }
            }
            Console.WriteLine("Delete successful");
        }

        public void GetAllCategories()
        {
            int numbering = 1;
            var categories = _repository.GetAllCategories();
            if(categories == null || !categories.Any())
            {
                Console.WriteLine("No existing category");
                return;
            }
            foreach(var category in categories)
            {
              Console.WriteLine($"{numbering}. {category.Name}");
                        
              Console.WriteLine(new string('-', 40));
                numbering++;   
            }
        }
       
       

        public void GetCategoryById(Guid id)
        {
           var category = _repository.GetCategoryById(id);
           if(category == null)
            {
                Console.WriteLine("category not found");
                return;
            }
            Console.WriteLine($@"{category.Name}
                                 {category.Id}
                                 {category.DateCreated:yyyy-MM-dd HH:mm:ss tt}
                                 {category.Description}");
        }

        public void SearchCategory(string searchTerm)
        {
            var numbering = 1;
            var categories = _repository.GetByDetail(searchTerm);
            if(categories == null || !categories.Any())
            {
                Console.WriteLine("No existing category");
                return;
            }
            foreach(var category in categories)
            {
              Console.WriteLine($"{numbering}. {category.Name}");
              Console.WriteLine(new string('-', 40));
                        numbering++;   
            }
        }

        public void UpdateCategory(Guid id, string name, string description)
        {
            var categoryToUpdate = _repository.UpdateCategory(id,name,description);
            if(categoryToUpdate == false)
            {
                Console.WriteLine("update fail");
                return;
            }
            Console.WriteLine("update successful");
        }

    }
}