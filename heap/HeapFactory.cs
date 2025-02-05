namespace sys_g_heap
{
    public static class HeapFactory
    {
        public static IHeap<T> CreateHeap<T>(bool isMinHeap, IComparer<T>? comparer = null)
        {
            comparer ??= isMinHeap
                ? Comparer<T>.Create((a, b) => Comparer<T>.Default.Compare(a, b))
                : Comparer<T>.Create((a, b) => Comparer<T>.Default.Compare(b, a));

            return isMinHeap ? new MinHeap<T>(comparer) : new MaxHeap<T>(comparer);
        }
    }
}