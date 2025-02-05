namespace sys_g_heap;

public interface IHeap<T>
{
    void Insert(T value);
    T ExtractRoot();
    T PeekRoot();
    void ChangeKey(int index, T newValue);
    void Delete(int index);
    void BuildHeap(T[] array);
    int Size();
    bool IsEmpty();
}