namespace sys_scheduler;

internal static class Program
{
    private static void Main()
    {
        const string connectionString = "mongodb://localhost:27017";
        const string databaseName = "TaskScheduler";
        const string collectionName = "Tasks";

        var taskScheduler = new TaskScheduler(connectionString, databaseName, collectionName);
        var currentDate = DateTime.Parse("2025-02-05 07:05:43").ToUniversalTime();

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
            Console.WriteLine("8. Get Finished Tasks Between Dates");
            Console.WriteLine("9. Get Overdue Tasks Count");
            Console.WriteLine("10. Get Last Three Tasks Created Before Days");
            Console.WriteLine("11. Get Last Three Same-Day Completed Tasks");
            Console.WriteLine("12. Get Task With Highest Priority Using Heap");
            Console.WriteLine("13. Get Task With Lowest Priority Using Heap");
            Console.WriteLine("14. Exit");
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
                    Console.WriteLine(taskScheduler.GetClosestTask(currentDate));
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
                    Console.WriteLine("\nEnter start date (YYYY-MM-DD):");
                    var startDate = DateTime.Parse(Console.ReadLine() ?? throw new InvalidOperationException()).ToUniversalTime();
                    Console.WriteLine("Enter end date (YYYY-MM-DD):");
                    var endDate = DateTime.Parse(Console.ReadLine() ?? throw new InvalidOperationException()).ToUniversalTime();
                    var count = taskScheduler.GetFinishedTasksCountBetweenDates(startDate, endDate);
                    Console.WriteLine($"Number of finished tasks between dates: {count}");
                    break;

                case 9:
                    Console.WriteLine($"\nNumber of overdue tasks: {taskScheduler.GetOverdueTasksCount(currentDate)}");
                    break;

                case 10:
                    Console.WriteLine("\nEnter number of days:");
                    var days = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                    Console.WriteLine("Last three tasks created before specified days:");
                    foreach (var task in taskScheduler.GetLastThreeTasksCreatedBeforeDays(currentDate, days))
                    {
                        Console.WriteLine(task);
                    }
                    break;

                case 11:
                    Console.WriteLine("\nLast three tasks completed same day as creation:");
                    foreach (var task in taskScheduler.GetLastThreeTasksCompletedSameDay())
                    {
                        Console.WriteLine(task);
                    }
                    break;
                
                case 12:
                    Console.WriteLine("\nHighest Priority Task:");
                    var highestPriorityTask = taskScheduler.GetHighestPriorityTask();
                    if (highestPriorityTask != null)
                        Console.WriteLine(highestPriorityTask);
                    else
                        Console.WriteLine("No tasks found.");
                    break;

                case 13:
                    Console.WriteLine("\nLowest Priority Task:");
                    var lowestPriorityTask = taskScheduler.GetLowestPriorityTask();
                    if (lowestPriorityTask != null)
                        Console.WriteLine(lowestPriorityTask);
                    else
                        Console.WriteLine("No tasks found.");
                    break;

                case 14:
                    return;

                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
        }
    }
}