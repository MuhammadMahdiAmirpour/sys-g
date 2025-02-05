using System.Globalization;
using MongoDB.Bson;

namespace sys_scheduler;

public class Task
{
    public ObjectId Id { get; set; } // MongoDB requires an _id field
    public required string Title { get; set; }
    public required string Description { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? FinishDate { get; set; }
    public PriorityLevel Priority { get; set; } // Lower number means higher priority (e.g., 1 > 2 > 3)

    public override string ToString()
    {
        return
            $"Title: {Title}, Description: {Description}, CreationDate: {CreationDate.ToString(CultureInfo.InvariantCulture)}, Due Date: {DueDate.ToString(CultureInfo.InvariantCulture)}, FinishDate: {FinishDate?.ToString()}, Priority: {Priority.ToString()}";
    }
}