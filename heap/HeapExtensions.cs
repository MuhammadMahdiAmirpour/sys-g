namespace sys_g_heap
{
    public static class HeapExtensions
    {
        // Generic method to convert IEnumerable<T> into a heap
        public static IHeap<T> ToHeap<T>(this IEnumerable<T> source, bool isMinHeap, IComparer<T> comparer = null) 
            where T : IComparable<T>
        {
            ArgumentNullException.ThrowIfNull(source);

            // Create a heap using the factory method with the specified comparer
            var heap = HeapFactory.CreateHeap<T>(isMinHeap, comparer);

            // Use LINQ to process the data and insert it into the heap
            foreach (var item in source)
            {
                heap.Insert(item);
            }

            return heap;
        }
    }
}