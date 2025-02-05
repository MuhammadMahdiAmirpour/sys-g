using sys_g_heap;

namespace sys_scheduler;

public class TaskHeap(IComparer<Task> comparer) : Heap<Task>(comparer)
{
    public override void Insert(Task value)
    {
        TheHeap.Add(value);
        HeapifyUp(TheHeap.Count - 1);
    }

    public override Task ExtractRoot()
    {
        if (TheHeap.Count == 0) throw new InvalidOperationException("Heap is empty");
        var root = TheHeap[0];
        var last = TheHeap[^1];
        TheHeap.RemoveAt(TheHeap.Count - 1);
        if (TheHeap.Count > 0)
        {
            TheHeap[0] = last;
            HeapifyDown(0);
        }
        return root;
    }

    public override Task PeekRoot()
    {
        if (TheHeap.Count == 0) throw new InvalidOperationException("Heap is empty");
        return TheHeap[0];
    }

    public override void ChangeKey(int index, Task newValue)
    {
        if (index < 0 || index >= TheHeap.Count) throw new ArgumentOutOfRangeException(nameof(index));
        var oldValue = TheHeap[index];
        TheHeap[index] = newValue;
        if (_comparer.Compare(newValue, oldValue) < 0) HeapifyUp(index);
        else HeapifyDown(index);
    }

    public override void Delete(int index)
    {
        if (index < 0 || index >= TheHeap.Count) throw new ArgumentOutOfRangeException(nameof(index));
        var lastElement = TheHeap[^1];
        TheHeap[index] = lastElement;
        TheHeap.RemoveAt(TheHeap.Count - 1);
        if (index < TheHeap.Count)
        {
            HeapifyDown(index);
            HeapifyUp(index);
        }
    }

    public override void BuildHeap(Task[] array)
    {
        TheHeap = new List<Task>(array);
        for (var i = (TheHeap.Count / 2) - 1; i >= 0; i--)
        {
            HeapifyDown(i);
        }
    }

    private new bool Compare(Task a, Task b) => _comparer.Compare(a, b) < 0;

    protected override void HeapifyUp(int index)
    {
        var parentIndex = (index - 1) / 2;
        while (index > 0 && Compare(TheHeap[index], TheHeap[parentIndex]))
        {
            Swap(index, parentIndex);
            index = parentIndex;
            parentIndex = (index - 1) / 2;
        }
    }

    protected override void HeapifyDown(int index)
    {
        while (true)
        {
            var leftChild = 2 * index + 1;
            var rightChild = 2 * index + 2;
            var targetChild = index;
            if (leftChild < TheHeap.Count && Compare(TheHeap[leftChild], TheHeap[targetChild]))
                targetChild = leftChild;
            if (rightChild < TheHeap.Count && Compare(TheHeap[rightChild], TheHeap[targetChild]))
                targetChild = rightChild;
            if (targetChild == index) break;
            Swap(index, targetChild);
            index = targetChild;
        }
    }
}