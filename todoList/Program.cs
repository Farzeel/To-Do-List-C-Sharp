using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;

namespace todoList
{
    internal class Program
    {
       public static int id = 0;
        public static string description;
        public static int noOfDays = 0;
        public static void AddTask( ref List<Task> tasks, int id , string description , DateTime day)
        {
            Task newTask = new Task(id, description, day);
            tasks.Add(newTask);
        }
        public static void DisplayTask(ref List<Task> tasks)
        {
            foreach(var task in tasks)
            {

              Console.WriteLine($"ID: {task.ID}, Description: {task.Description}, Due-Date: {task.DueDate}");
                Console.WriteLine("..................................");
            }
        }

        public static void DeleteTask(ref List<Task> tasks , int id)
        {
           Task deleteId =  tasks.FirstOrDefault(task => task.ID == id);
            if (deleteId != null)
            {
                tasks.Remove(deleteId);
                Console.WriteLine("...................");
                Console.WriteLine("Task Deleted Sucessfully");
                Console.WriteLine("...................");
            }
            else
            {
                Console.WriteLine("...................");
                Console.WriteLine($"No task found of id {id}");
                Console.WriteLine("...................");
            }
        }

        public static void UpdateMessage()
        {
            Console.WriteLine("...................");
            Console.WriteLine("Task Updated Sucessfully");
            Console.WriteLine("...................");
        }

        static void SaveTasksToFile(List<Task> tasks, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (Task task in tasks)
                {
                    writer.WriteLine($"{task.ID},{task.Description},{task.DueDate}");
                }
            }
        }

        public static void LoadTaskFromFile(string filePath)
        {
            if(File.Exists(filePath))
            {
                string text  = File.ReadAllText(filePath);
                Console.WriteLine(text);

            }else { Console.WriteLine("no file Exists"); }
        }
        static void Main(string[] args)
        {
            string pathh = "tasks.txt";

            List<Task> tasks = new List<Task>();
            LoadTaskFromFile(pathh);
            
            Console.WriteLine("Welcome to Todo List App");
            Console.WriteLine(".........................");
            
            Boolean notExit = true;
            
            while(notExit) {
             Console.WriteLine("Press 1: for Add a new task\r\nPress 2: for View existing tasks\r\nPress 3: for Edit a task\r\nPress 4: for Delete a task\r\nPress 5: for Exit the application");
            Console.WriteLine(".........................");

                Console.WriteLine("What would you like to do");
                string option = Console.ReadLine();
                
                if(option =="1" || option=="2" || option == "3" || option == "4" || option == "5")
                {


                    if (option == "1")
                    {
                        Boolean desc = true;
                        while (desc)
                        {
                            Console.WriteLine("Enter The Task Description");
                            description = Console.ReadLine();

                            if (description.Length < 10)
                            {
                                Console.WriteLine("Description must be 10 chrachter Long");
                            }
                            else
                            {
                                desc = false;
                            }
                        }

                        try
                        {
                            Boolean idTrue = true;
                            while (idTrue)
                            {
                                Console.WriteLine("Enter The ID For The Task");
                                id = Convert.ToInt32(Console.ReadLine());
                                if (id <= 0)
                                {
                                    Console.WriteLine("Id Must be Graeter than zero");
                                    continue;

                                }
                                if (tasks.Any(task => task.ID == id))
                                {
                                    Console.WriteLine($"ID {id} is already in use. Please select a different ID.");
                                    
                                }
                                else
                                {
                                    idTrue = false;
                                }
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Please Enter Numebr not a string");
                            continue;
                        }



                        try
                        {
                            Boolean noDayTrue = true;
                            while (noDayTrue)
                            {
                                Console.WriteLine("In How many Days You Want to Complete This Task 1,2,3....");
                                noOfDays = Convert.ToInt32(Console.ReadLine());
                                if (noOfDays < 0)
                                {
                                    Console.WriteLine("no of days must be a positive value");
                                }
                                else
                                {
                                    noDayTrue = false;
                                }

                            }

                        }
                        catch (Exception)
                        {
                            Console.WriteLine("please enter a nummber");
                            continue;
                        }
                        DateTime dueDate = DateTime.Now.AddDays(noOfDays);

                      
                        AddTask(ref tasks, id, description, dueDate);
                        Console.WriteLine("Task Has Been Added Sucessfully");
                        Console.WriteLine("...............................");



                    }
                    else if (option == "2")
                    {
                        if (tasks.Count > 0)
                        {
                            DisplayTask(ref tasks);
                        }
                        else
                        {
                            Console.WriteLine("Task List Is Empty.No task is available to Show !");
                            Console.WriteLine("............................");
                        }
                    }
                    else if (option == "3")
                    {
                        if (tasks.Count > 0)
                        {

                            try
                            {
                                Console.WriteLine("Enter an Task Id you Want to Edit");
                                id = Convert.ToInt32(Console.ReadLine());
                                Task updateId = tasks.FirstOrDefault(task => task.ID == id);
                                if (updateId != null)
                                {
                                    Console.WriteLine($"Edit Task with the id {id}");
                                    Console.WriteLine($"1. Description {updateId.Description}");
                                    Console.WriteLine($"2. Due-Date {updateId.DueDate}");
                                    Console.WriteLine($"3. Both Description {updateId.Description} Due-Date {updateId.DueDate}");
                                    Console.WriteLine("........................................");
                                    Console.WriteLine("Eneter the Property You Want To Edit (or 0 to Cancel)");
                                    int choice = Convert.ToInt32(Console.ReadLine());

                                    switch (choice)
                                    {
                                        case 1:
                                            Console.WriteLine("Enter The Task Description");
                                            description = Console.ReadLine();

                                            if (description.Length < 10)
                                            {
                                                Console.WriteLine("Description must be 10 chrachter Long");
                                            }
                                            else
                                            {

                                            updateId.Description = description;
                                                UpdateMessage();
                                            }
                                            break;

                                        case 2:
                                            Console.WriteLine("In How many Days You Want to Complete This Task 1,2,3....");
                                            noOfDays = Convert.ToInt32(Console.ReadLine());
                                            if (noOfDays < 0)
                                            {
                                                Console.WriteLine("no of days must be a positive value");
                                            }
                                            else
                                            {
                                                DateTime dateTime = DateTime.Now.AddDays(noOfDays);
                                            updateId.DueDate = dateTime;
                                                UpdateMessage();
                                            }
                                            break;
                                        case 3:
                                            Console.WriteLine("Enter The Task Description");
                                            description = Console.ReadLine();

                                            if (description.Length < 10)
                                            {
                                                Console.WriteLine("Description must be 10 chrachter Long");
                                                continue;
                                            }
                                            else
                                            {

                                                updateId.Description = description;
                                            }
                                            Console.WriteLine("In How many Days You Want to Complete This Task 1,2,3....");
                                            noOfDays = Convert.ToInt32(Console.ReadLine());
                                            if (noOfDays < 0)
                                            {
                                                Console.WriteLine("no of days must be a positive value");
                                                continue;
                                            }
                                            else
                                            {
                                                DateTime dateTime = DateTime.Now.AddDays(noOfDays);
                                                updateId.DueDate = dateTime;
                                            }
                                            UpdateMessage();
                                            break;
                                        default:
                                            Console.WriteLine("Invaild Option No Changes Made");
                                            break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("...................");
                                    Console.WriteLine($"No task found of id {id}");
                                    Console.WriteLine("...................");
                                }
                           
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }

                        }
                        else
                        {
                            Console.WriteLine("....................");
                            Console.WriteLine(" list is already Empty");
                            Console.WriteLine("....................");
                        }
                    }
                    else if (option == "4")
                    {
                        if (tasks.Count>0)
                        {
                            try
                            {
                              

                                    Console.WriteLine("Your Tasks");
                                    DisplayTask(ref tasks);
                                    Console.WriteLine("Enetr the Id of Task You Want to Delete");
                                    id = Convert.ToInt32(Console.ReadLine());
                           
                                    DeleteTask(ref tasks, id);
                                  
                                
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("...................");
                                Console.WriteLine("please Enter a Valid Id");
                                Console.WriteLine("...................");
                            }
                        }
                        else
                        {
                            Console.WriteLine("....................");
                            Console.WriteLine(" list is already Empty");
                            Console.WriteLine("....................");
                        }
                    }
                    else if (option == "5")
                    {
                        SaveTasksToFile( tasks, pathh);
                        Console.WriteLine("Tasks Save To the file. Exiting the program");
                        notExit = false;
                    }
                }
                else
                {
                            Console.WriteLine("....................");
                    Console.WriteLine("please Enter a Valid Option");
                            Console.WriteLine("....................");
                }

            }




            



        }
    }
}
