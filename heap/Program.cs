namespace sys_g_heap;

internal static class Program
{
    private static void Main()
    {
        Console.WriteLine("Choose Heap Type: 1 for Min-Heap, 2 for Max-Heap");
        var choice = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
        var isMinHeap = choice == 1;

        var heap = HeapFactory.CreateHeap<int>(isMinHeap);

        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Insert Element");
            Console.WriteLine("2. Extract Root");
            Console.WriteLine("3. Peek Root");
            Console.WriteLine("4. Change Key");
            Console.WriteLine("5. Delete Element");
            Console.WriteLine("6. Build Heap from Array");
            Console.WriteLine("7. Visualize Heap");
            Console.WriteLine("8. Exit");
            Console.Write("Enter your choice: ");
            var option = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            switch (option)
            {
                case 1:
                    Console.Write("Enter element to insert: ");
                    var insertValue = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                    heap.Insert(insertValue);
                    break;

                case 2:
                    try
                    {
                        Console.WriteLine("Extracted Root: " + heap.ExtractRoot());
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case 3:
                    try
                    {
                        Console.WriteLine("Peek Root: " + heap.PeekRoot());
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case 4:
                    Console.Write("Enter index to change: ");
                    var index = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                    Console.Write("Enter new value: ");
                    var newValue = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                    heap.ChangeKey(index, newValue);
                    break;

                case 5:
                    Console.Write("Enter index to delete: ");
                    var deleteIndex = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                    heap.Delete(deleteIndex);
                    break;

                case 6:
                    Console.Write("Enter array elements separated by spaces: ");
                    var inputArray = Console.ReadLine()?.Split(' ') ?? throw new InvalidOperationException();
                    var array = Array.ConvertAll(inputArray, int.Parse);
                    heap.BuildHeap(array);
                    break;

                case 7:
                    Console.WriteLine("Visualizing Heap...");
                    ((Heap<int>)heap).PrintHeapAsArray();
                    ((Heap<int>)heap).PrintHeapAsTree();
                    break;

                case 8:
                    return;

                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
        }
    }
}