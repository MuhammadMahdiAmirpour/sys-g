using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace sys_scheduler;
public class Task : ITaskBase
{
    [BsonId]
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? FinishDate { get; set; }
    public PriorityLevel Priority { get; set; }

    public override string ToString()
    {
        return $"Task[Title: {Title}, " +
               $"Description: {Description}, " +
               $"Priority: {Priority}, " +
               $"Created: {CreationDate:yyyy-MM-dd HH:mm:ss}, " +
               $"Due: {DueDate:yyyy-MM-dd HH:mm:ss}, " +
               $"Finished: {(FinishDate.HasValue ? FinishDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "Not finished")}]";
    }
}