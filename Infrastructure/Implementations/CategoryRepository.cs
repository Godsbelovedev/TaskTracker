using System.Text.Json;
using Microsoft.VisualBasic;
using TaskTracker.Infrastructure.Interfaces;
using TaskTracker.Model.Entities;

namespace TaskTracker.Application.Implementations
{
    public class CategoryRepository : ICategoryRepositoryInterface
    {
        private void WriteCategoriesToFile(List<Category> categories)
        {

            var jsonConvert = JsonSerializer.Serialize(categories, new JsonSerializerOptions
            { WriteIndented = true });
            var folderPath = Path.Combine(Environment.CurrentDirectory, "FileStoragePaths");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string filePath = Path.Combine(folderPath, "categories.json");

            File.WriteAllText(filePath, jsonConvert);

        }

        private List<Category> ReadCategoriesFromFile()
        {
            var folderPath = Path.Combine(Environment.CurrentDirectory, "FileStoragePaths");

            if (!Directory.Exists(folderPath))
            {
                return new List<Category>();
            }

            string filePath = Path.Combine(folderPath, "categories.json");
            if(!File.Exists(filePath))
            {
                return new List<Category>();
            }
            var categoriesInJson = File.ReadAllText(filePath);

            if (string.IsNullOrEmpty(categoriesInJson))
            {
                return new List<Category>();
            }

            var listOfCategory = JsonSerializer.Deserialize<List<Category>>(categoriesInJson);

            if (listOfCategory == null || !listOfCategory.Any())
            {
                return new List<Category>();
            }
            
            return listOfCategory;
        }
        public bool AddCategory(Category category)
        {
            var listOfCategories = new List<Category>();
            if (category == null)
                return false;
            listOfCategories.Add(category);
            WriteCategoriesToFile(listOfCategories);
            return true;
        }

        public bool DeleteCategory(Guid id)
        {
            var listOfCategories = ReadCategoriesFromFile();
            foreach (var item in listOfCategories)
            {
                if (item.Id == id)
                {
                    listOfCategories.Remove(item);
                    WriteCategoriesToFile(listOfCategories);
                    return true;
                }
            }
            return false;
        }

        public List<Category> GetAllCategories()
        {
            return ReadCategoriesFromFile();
        }

        public List<Category> GetByDetail(string searchItem)
        {
            var listOfCategory = ReadCategoriesFromFile();
            var result = new List<Category>();
            if (listOfCategory == null ||
            !listOfCategory.Any())
            {
                return result;
            }
            foreach (var item in listOfCategory)
            {
                if (item.Name != null && item.Name.Contains
                (searchItem, StringComparison.OrdinalIgnoreCase) ||
                item.Description != null && item.Description.
                Contains(searchItem, StringComparison.OrdinalIgnoreCase)
                )
                {
                    result.Add(item);
                }
            }
            return result;
        }


        public Category? GetCategoryById(Guid id)
        {
            var listOfCategories = ReadCategoriesFromFile();
            foreach (var item in listOfCategories)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            return null;
        }

        public bool UpdateCategory(Guid id, string name, string description)
        {
            var listOfCategories = ReadCategoriesFromFile();
            foreach (var item in listOfCategories)
            {
                if (item.Id == id)
                {
                    item.Description = description;
                    item.Name = name;
                    WriteCategoriesToFile(listOfCategories);
                    return true;
                }
            }
            return false;
        }

    }
}