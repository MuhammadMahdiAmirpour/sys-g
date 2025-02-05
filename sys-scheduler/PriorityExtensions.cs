namespace sys_scheduler;

public static class PriorityExtensions
{
    public static int PriorityToInt(this PriorityLevel priority)
    {
        return (int)priority;
    }
}