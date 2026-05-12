using TaskTracker.Infrastructure.Interfaces;

namespace TaskTracker
{
    public class ConsoleHelper
    {
        public static int ValidateChoice(int min, int max)
        {
            int choice;
            if(min == max)
            {
                Console.WriteLine($"input {max}");
             bool isConfirmChoice = int.TryParse(Console.ReadLine(),
                                                 out choice);
             while(!isConfirmChoice || choice < max || choice > max)
             {
                Console.WriteLine($@"invalid option
                        input option between {max}");
                isConfirmChoice = int.TryParse(Console.ReadLine(),
                                                 out choice);
                Console.Beep();
             }
            }
            else
            {
                 Console.WriteLine($"input an option between {min}-{max}");
             bool isConfirmChoice = int.TryParse(Console.ReadLine(),
                                                 out choice);
            
           while(!isConfirmChoice || choice < min || choice > max)
            {
                Console.WriteLine($@"invalid option
                        input an option between {min}-{max}");
                isConfirmChoice = int.TryParse(Console.ReadLine(),
                                                 out choice);
                Console.Beep();
            }
            }
           
            return choice;
        }

        public static bool AuthorizeTaskViewal(Guid id)
        {
            TodoItemsRepository repo = new TodoItemsRepository();
            var listOfTasks = repo.GetAllToDoItems();
            Console.WriteLine("input task password");
            string passWord = Console.ReadLine();
            foreach (var item in listOfTasks)
            {
                if (item.Id == id)
                {
                  if(item.PassWord != passWord)
                    {
                        return false;
                    } 
                }
            }
                    return true;  
        }
        public static bool LockACategory(Guid id, string password)
        {
            
            TodoItemsRepository repo = new TodoItemsRepository();
            var listOfTasks = repo.GetAllToDoItems();
            foreach (var item in listOfTasks)
            {
                if (item.Id == id)
                {
                   
                    item.PassWord = password;
                    return true;
                }
            }
                    return false;  
        }
    }
}