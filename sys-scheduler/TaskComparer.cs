namespace sys_scheduler;

public class TaskComparer : IComparer<Task>
{
    public int Compare(Task? x, Task? y)
    {
        // Compare by DueDate first
        var dueDateComparison = x.DueDate.CompareTo(y.DueDate);
        return dueDateComparison != 0 ? dueDateComparison :
            // If DueDates are equal, compare by Priority (lower number means higher priority)
            ((int)x.Priority).CompareTo((int)y.Priority);
    }
}