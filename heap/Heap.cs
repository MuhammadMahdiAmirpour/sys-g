namespace sys_g_heap;

public abstract class Heap<T> : IHeap<T>
{
    public List<T> TheHeap = new();
    public readonly IComparer<T> _comparer;

    protected Heap(IComparer<T>? comparer)
    {
        _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
    }

    public int Size() => TheHeap.Count;
    public bool IsEmpty() => TheHeap.Count == 0;

    public abstract void Insert(T value);
    public abstract T ExtractRoot();
    public abstract T PeekRoot();
    public abstract void ChangeKey(int index, T newValue);
    public abstract void Delete(int index);
    public abstract void BuildHeap(T[] array);

    protected void Swap(int i, int j)
    {
        (TheHeap[i], TheHeap[j]) = (TheHeap[j], TheHeap[i]);
    }

    protected bool Compare(T a, T b) => _comparer.Compare(a, b) < 0;

    protected abstract void HeapifyUp(int index);
    protected abstract void HeapifyDown(int index);

    public void PrintHeapAsArray()
    {
        Console.WriteLine("Heap Array: [" + string.Join(", ", TheHeap) + "]");
    }

    public void PrintHeapAsTree()
    {
        if (TheHeap.Count == 0)
        {
            Console.WriteLine("Heap is empty.");
            return;
        }
        Console.WriteLine("Heap Tree:");
        PrintHeapTree(0, "", true);
    }

    private void PrintHeapTree(int index, string prefix, bool isLeft)
    {
        if (index >= TheHeap.Count) return;
        Console.WriteLine(prefix + (isLeft ? "├── " : "└── ") + TheHeap[index]);
        int leftChild = 2 * index + 1;
        int rightChild = 2 * index + 2;
        PrintHeapTree(leftChild, prefix + (isLeft ? "│   " : "    "), true);
        PrintHeapTree(rightChild, prefix + (isLeft ? "│   " : "    "), false);
    }
}