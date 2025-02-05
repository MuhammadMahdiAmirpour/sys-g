namespace sys_scheduler;

public interface ITaskBase
{
    DateTime DueDate { get; }
    PriorityLevel Priority { get; }
}

// Make the interface covariant with out
public interface ITaskProvider<out T> where T : ITaskBase
{
    T GetTask();
    IEnumerable<T> GetTasks();
}
