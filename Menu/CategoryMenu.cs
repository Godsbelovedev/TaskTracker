using TaskTracker.Application.Implementations;
using TaskTracker.Application.Interfaces;
using TaskTracker.Infrastructure.Interfaces;
using TaskTracker.Model.Entities;

namespace TaskTracker.Menu
{
    public class CategoryMenu
    {
        private readonly ICategoryServiceInterface _categoryService;
        private readonly ICategoryRepositoryInterface _categoryRepositories;
        private readonly ITodoServiceInterface _todoService;

        public CategoryMenu()
        {
            _categoryService = new CategoryService();
            _categoryRepositories = new CategoryRepository();
            _todoService = new TodoService();
        }
        public void Start()
        {
            while (true)
            {
                Console.WriteLine("WELCOME TO TASK MENU");
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("1 ➤ Create A Category");
                Console.WriteLine("2 ➤ View All Category");
                Console.WriteLine("3 ➤ View Overdue Tasks");
                Console.WriteLine("0 ➤ return to main Menu");
                Console.WriteLine("-----------------------------------------");

                int choice = ConsoleHelper.ValidateChoice(0, 3);

                switch (choice)
                {
                    case 1:
                        AddCategory();
                        break;

                    case 2:
                        ViewAllCategories();
                        break;

                    case 3:
                        SearchCategory();
                        break;

                    case 0:
                        return;

                }
            }
        }

        private void AddCategory()
        {
            Thread.Sleep(2000);
            Console.WriteLine("welcome!");

            Console.WriteLine("input category name (required)");
            string categoryName = Console.ReadLine()?.Trim();

            while (string.IsNullOrWhiteSpace(categoryName))
            {
                Console.WriteLine("input a valid name. category name cant be empty");
                categoryName = Console.ReadLine()?.Trim();
            }

            Console.WriteLine("input category description or press enter to proceed(optional)");
            string categoryDescription = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(categoryDescription))
            {
                categoryDescription = "no category";
                Console.WriteLine("category set to 'NO CATEGORY'");
            }

            Console.WriteLine("input category password or press enter to proceed (optional)");
            string categoryPassword = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(categoryPassword))
            {
                categoryPassword = "";
                Console.WriteLine("no password set");
            }

            var category = new Category(categoryName, categoryDescription, categoryPassword);
            _categoryService.AddCategory(category);
        }
        private void ViewAllCategories()
        {
            var allCategories = _categoryRepositories.GetAllCategories();
            _categoryService.GetAllCategories();
            if (allCategories.Count > 1)
            {
                Console.WriteLine($@"do you wish to view, delete or update a task?
                    
                        SELECT:
                            1 to VIEW
                            2 to UPDATE
                            3 to DELETE
                         OR 0 to EXIT");
                int option = ConsoleHelper.ValidateChoice(0, 3);
                if (option == 1) //view
                {
                    Console.WriteLine($@"SELECT from 1-{allCategories.Count}
                                        the task you want to VIEW");
                    int choice = ConsoleHelper.ValidateChoice(1, allCategories.Count);

                    _categoryService.GetCategoryById(allCategories[choice - 1].Id);
                    Console.WriteLine($@"do you wish to view all the tasks under this category?
                                       
                                       SELECT '1' For YES and '2' For NO:

                                              1. YES
                                              2. NO ");
                    int taskChoice = ConsoleHelper.ValidateChoice(1, 2);
                    if(taskChoice == 1)
                    {
                        _todoService.GetTasksByCategoryId(allCategories[choice - 1].Id);
                    }
                    else
                    {
                        return;
                    }

                }
                else if (option == 2) //update
                {
                    Console.WriteLine($@"SELECT from 1-{allCategories.Count}
                                        the Category  you want to UPDATE");
                    int choice = ConsoleHelper.ValidateChoice(1, allCategories.Count);

                    Console.WriteLine($@"will you like to change CATEGORY NAME?
                                            click 1 to change CATEGORY NAME 
                                            click 2 to contiue (existing NAME will be retained)");
                    int categoryNameChoice = ConsoleHelper.ValidateChoice(1, 2);
                    string categoryName;

                    if (categoryNameChoice == 1)
                    {
                        categoryName = Console.ReadLine().Trim();
                    }

                    else
                    {
                        categoryName = allCategories[choice - 1].Name;
                        Console.WriteLine("existing name is retained");
                    }

                    Console.WriteLine($@"will you like to change CATEGORY DESCRIPTION?
                                            click 1 to change CATEGORY DESCRIPTION 
                                            click 2 to contiue (existing CATEGORY will be retained)");
                    int descriptionChoice = ConsoleHelper.ValidateChoice(1, 2);
                    string? categoryDescription;

                    if (descriptionChoice == 1)
                    {
                        categoryDescription = Console.ReadLine().Trim();
                    }

                    else
                    {
                        categoryDescription = allCategories[choice - 1].Description;
                        Console.WriteLine("existing description is retained");
                    }
                    Console.WriteLine("input category password or press enter to proceed (optional)");
                    string categoryPassword = Console.ReadLine()?.Trim();

                    if (string.IsNullOrWhiteSpace(categoryPassword))
                    {
                        categoryPassword = "";
                        Console.WriteLine("no password set");
                    }

                    var category = new Category(categoryName, categoryDescription, categoryPassword);
                    _categoryService.AddCategory(category);
                }
                else if (option == 3)
                {
                    Console.WriteLine($@"SELECT from 1-{allCategories.Count}
                                            the task you want to DELETE");
                    int choice = ConsoleHelper.ValidateChoice(1, allCategories.Count);

                    _categoryService.DeleteCategory(allCategories[choice - 1].Id);
                }
                else
                {
                    return;
                }
            }
        }
        private void SearchCategory()
        {
            Thread.Sleep(2000);
            Console.WriteLine("input the category name to search");
            string input = Console.ReadLine();
            _categoryService.SearchCategory(input);
        }
    }

}
