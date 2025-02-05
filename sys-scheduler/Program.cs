namespace sys_scheduler;

internal static class Program
{
    private static void Main()
    {
        const string connectionString = "mongodb://localhost:27017";
        const string databaseName = "TaskScheduler";
        const string collectionName = "Tasks";

        var taskScheduler = new TaskScheduler(connectionString, databaseName, collectionName);

        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Show All Tasks");
            Console.WriteLine("2. Get Closest Task");
            Console.WriteLine("3. Toggle Heap Usage");
            Console.WriteLine("4. Get Unfinished Task Count by Priority");
            Console.WriteLine("5. Get Number of Tasks");
            Console.WriteLine("6. Get Number of Finished Tasks");
            Console.WriteLine("7. Get Number of Unfinished Tasks");
            Console.WriteLine("8. Exit");
            Console.Write("Enter your choice: ");
            var option = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            switch (option)
            {
                case 1:
                    Console.WriteLine("All Tasks in Database:");
                    foreach (var task in taskScheduler.LoadTasks())
                    {
                        Console.WriteLine(task);
                    }
                    break;

                case 2:
                    Console.WriteLine("\nClosest Task:");
                    Console.WriteLine(taskScheduler.GetClosestTask(DateTime.Now));
                    break;

                case 3:
                    taskScheduler.ToggleHeapUsage();
                    break;

                case 4:
                    Console.WriteLine("\nUnfinished Task Count by Priority:");
                    foreach (var priorityCount in taskScheduler.GetUnfinishedTaskCountByPriority())
                    {
                        Console.WriteLine(priorityCount);
                    }
                    break;

                case 5:
                    Console.WriteLine($"\nTotal number of tasks: {taskScheduler.GetNumberOfTasks()}");
                    break;

                case 6:
                    Console.WriteLine($"\nNumber of finished tasks: {taskScheduler.GetNumberOfFinishedTasks()}");
                    break;

                case 7:
                    Console.WriteLine($"\nNumber of unfinished tasks: {taskScheduler.GetNumberOfUnfinishedTasks()}");
                    break;

                case 8:
                    return;

                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
        }
    }
}
