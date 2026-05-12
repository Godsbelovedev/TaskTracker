// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using TaskTracker.Menu;
// Random random = new Random();
// int nextValue = random.Next(0, 10);

// for(int i = nextValue; i <= 100; i++)
// {
//     Console.Clear();
//     Console.WriteLine($@"App {i}% loading");
//     Thread.Sleep(200);
// }
// string message = "welcome";
// for(int i = nextValue; i <= 30; i++)
// {
//     Console.Clear();
//     Console.SetCursorPosition(i, 5);
//     Console.WriteLine(message);
//     Console.SetCursorPosition(i, 5);
//     Console.WriteLine(message);
//     Thread.Sleep(500);
// }
MainMenu menu = new MainMenu();
menu.Start();
