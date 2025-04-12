using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Program
{
    static List<TaskItem> tasks = new List<TaskItem>();

    static void Main()
    {
        while (true)
        {
            LoadTasksFromFile();
            Console.WriteLine("\n1. Add Task\n2. View Tasks\n3. Mark Completed\n4. Remove Task\n5. Exit");
            Console.WriteLine("Choose: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1": AddTask(); break;
                case "2": ViewTask(); break;
                case "3": MarkCompleted(); break;
                case "4": RemoveTask(); break;
                case "5": SaveTasksToFile();  return;
                default: Console.WriteLine("Invalid option."); break;
            }
        }
    }

    static void AddTask()
    {
        Console.WriteLine("Enter task description: ");
        string? desc = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(desc))
        {
            Console.WriteLine("Task description cannot be empty.");
            return;
        }
        tasks.Add(new TaskItem { Description = desc, IsCompleted = false });
        Console.WriteLine("Task added.");
    }

    static void ViewTask() { 
     if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks.");
            return;
        }
     for (int i =0; i <tasks.Count; i++)
        {
            string status = tasks[i].IsCompleted ? "[X]" : "[ ]";
            Console.WriteLine($"{i + 1}. {status} {tasks[i].Description}");
        }
    }

    static void MarkCompleted() 
    {
        ViewTask();
        if (tasks.Count == 0)
        {
            Console.WriteLine("Since there is no task you cannot complete any");
            return;
        }
        Console.WriteLine("Enter task number to mark complete: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index <= tasks.Count && index > 0)
        {
            tasks[index - 1].IsCompleted = true;
            Console.WriteLine("Marked as completed.");
        }
        else Console.WriteLine("Invalid number.");
    }

    static void RemoveTask()
    {
        ViewTask();
        if (tasks.Count == 0)
        {
            Console.WriteLine("Since there is no task you cannot remove any");
            return;
        }
        Console.Write("Enter task number to remove: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index <= tasks.Count && index > 0)
        {
            tasks.RemoveAt(index - 1);
            Console.WriteLine("Task removed.");
        }
        else Console.WriteLine("Invalid number.");
    }

    static void SaveTasksToFile()
    {
        string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions {WriteIndented = true });
        File.WriteAllText("tasks.json", json);
    }

    static void LoadTasksFromFile()
    {
        if (File.Exists("tasks.json"))
        {
            string json = File.ReadAllText("tasks.json");
            tasks = JsonSerializer.Deserialize<List<TaskItem>>(json);
        }
    }
}
