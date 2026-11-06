using System.Collections.Generic;

public class PriorityQueue<T>
{
    private List<(T item, float priority)> elements = new List<(T, float)>();
    public int Count => elements.Count;

    public void Enqueue(T item, float priority)
    {
        elements.Add((item, priority));
        HeapUp(elements.Count - 1);
    }

    public T Dequeue()
    {
        if (elements.Count == 0)
            throw new System.InvalidOperationException("The priority queue is empty.");

        T item = elements[0].item;
        elements[0] = elements[elements.Count - 1];
        elements.RemoveAt(elements.Count - 1);
        
        if(elements.Count > 0) HeapDown(0);

        return item;
    }

    public (T item, float priority) Peek()
    {
        if (elements.Count == 0)
            throw new System.InvalidOperationException("The priority queue is empty.");

        return elements[0];
    }

    private void HeapUp(int index)
    {
        while (index > 0)
        {
            int parentIndex = (index - 1) / 2;
            if (elements[index].priority >= elements[parentIndex].priority)
                break;

            (elements[index], elements[parentIndex]) = (elements[parentIndex], elements[index]);
            index = parentIndex;
        }
    }

    private void HeapDown(int index)
    {
        int lastIndex = elements.Count - 1;
        while (true)
        {
            int leftChildIndex = 2 * index + 1;
            int rightChildIndex = 2 * index + 2;
            int smallestIndex = index;

            if (leftChildIndex <= lastIndex && elements[leftChildIndex].priority < elements[smallestIndex].priority)
                smallestIndex = leftChildIndex;

            if (rightChildIndex <= lastIndex && elements[rightChildIndex].priority < elements[smallestIndex].priority)
                smallestIndex = rightChildIndex;

            if (smallestIndex == index)
                break;

            (elements[index], elements[smallestIndex]) = (elements[smallestIndex], elements[index]);
            index = smallestIndex;
        }
    }
}
