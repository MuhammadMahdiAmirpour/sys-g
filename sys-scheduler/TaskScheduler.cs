using MongoDB.Driver;
using sys_g_heap;

namespace sys_scheduler;

public class TaskScheduler
{
    private readonly IMongoCollection<Task> _tasks;
    private readonly IHeap<Task> _taskHeap; // Heap for managing tasks
    private bool _useHeap; // Flag to toggle heap usage

    public TaskScheduler(string connectionString, string databaseName, string collectionName)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _tasks = database.GetCollection<Task>(collectionName);

        CleanCollection(_tasks);
        InsertDummyData();

        // Initialize the heap with a MinHeap using the TaskComparer
        _taskHeap = new TaskHeap(new TaskComparer());
        LoadTasksIntoHeap();
    }

    private void CleanCollection(IMongoCollection<Task> tasks)
    {
        var filter = Builders<Task>.Filter.Empty;
        tasks.DeleteMany(filter);
    }

    private IEnumerable<Task> GenerateDummyTasks(int count)
    {
        var random = new Random();
        var titles = new[] { "Complete report", "Prepare presentation", "Review feedback", "Submit expenses", "Update documentation" };
        var descriptions = new[] { "Urgent", "Important", "Can be delayed", "Low priority", "Needs attention" };

        for (var i = 0; i < count; i++)
        {
            var creationDate = DateTime.Now.AddDays(-random.Next(1, 30));
            var dueDate = creationDate.AddDays(random.Next(1, 14));
            yield return new Task
            {
                Title = $"{titles[random.Next(titles.Length)]} {i + 1}",
                Description = descriptions[random.Next(descriptions.Length)],
                CreationDate = creationDate,
                DueDate = dueDate,
                FinishDate = random.Next(2) == 0 ? null : creationDate.AddDays(random.Next((dueDate - creationDate).Days)),
                Priority = (PriorityLevel)random.Next(1, 4)
            };
        }
    }

    private void InsertDummyData(int count = 20)
    {
        var dummyTasks = GenerateDummyTasks(count);
        _tasks.InsertMany(dummyTasks);
    }

    private void LoadTasksIntoHeap()
    {
        foreach (var task in LoadTasks())
        {
            _taskHeap.Insert(task); // Insert tasks into the heap
        }
    }

    public IEnumerable<Task> LoadTasks()
    {
        var filter = Builders<Task>.Filter.Empty;
        var cursor = _tasks.Find(filter);
        foreach (var task in cursor.ToEnumerable())
        {
            yield return task;
        }
    }

    public Task? GetClosestTask(DateTime currentDate)
    {
        if (_useHeap)
        {
            // Use the heap to find the closest task
            var heapCopy = (TaskHeap)_taskHeap; // Cast to TaskHeap for access to internal data
            return FindClosestTaskFromHeap(heapCopy, currentDate);
        }
        else
        {
            // Use LINQ-based approach
            return _tasks.AsQueryable()
                .Where(t => t.DueDate >= currentDate)
                .OrderBy(t => t.DueDate)
                .ThenBy(t => (int)t.Priority)
                .FirstOrDefault();
        }
    }

    private Task? FindClosestTaskFromHeap(TaskHeap heap, DateTime currentDate)
    {
        return heap.TheHeap.FirstOrDefault(task => task.DueDate >= currentDate);
    }

    public void ToggleHeapUsage()
    {
        _useHeap = !_useHeap;
        Console.WriteLine("Heap usage is now: " + (_useHeap ? "Enabled" : "Disabled"));
    }

    public int GetNumberOfTasks()
    {
        return _tasks.AsQueryable().Count();
    }

    public int GetNumberOfFinishedTasks()
    {
        return _tasks.AsQueryable().Count(t => t.FinishDate != null);
    }

    public int GetNumberOfUnfinishedTasks()
    {
        return _tasks.AsQueryable().Count(t => t.FinishDate == null);
    }

    public IEnumerable<PriorityTaskCount> GetUnfinishedTaskCountByPriority()
    {
        return _tasks.AsQueryable()
            .Where(t => t.FinishDate == null)
            .GroupBy(t => t.Priority, (priority, tasks) => new PriorityTaskCount
            {
                Priority = priority,
                Count = tasks.Count()
            })
            .OrderBy(g => (int)g.Priority) // Use Queryable.OrderBy
            .ToList();
    }
}