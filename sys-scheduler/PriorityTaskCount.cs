namespace sys_scheduler;

public class PriorityTaskCount
{
    public PriorityLevel Priority { get; set; }
    public int Count { get; set; }

    public override string ToString()
    {
        return $"Priority: {Priority}, Count: {Count}";
    }
}