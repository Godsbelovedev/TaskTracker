namespace TaskTracker.Model.Entities
{
    public class Category
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = default!;
        public string? Description {get; set; }
        public string? PassWord{get; set;}
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public Category(string name, string? description, string? passWord)
        {
            Name = name;
            Description = description;
            PassWord = passWord;
        }
    }
}