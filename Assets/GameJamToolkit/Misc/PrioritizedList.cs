using System.Collections.Generic;
using UnityEngine;

public class PrioritizedListElement<T> where T : class
{
    public readonly int priority = 0;
    public readonly T value;

    public PrioritizedListElement(T value, int priority = 0) {
        this.value = value;
        this.priority = priority;
    }
}

public class PrioritizedList<T> where T : class
{
    [SerializeField] private List<PrioritizedListElement<T>> list;
    [SerializeField] private int count;

    public int Count
    {
        get { return list.Count; }
        private set { }
    }

    public PrioritizedList() {
        list = new List<PrioritizedListElement<T>>();
        count = 0;
    }

    public T Top() {
        if (count <= 0) return null;
        else return list[count - 1].value;
    }

    public void Add(T value, int priority = 0) {
        list.Add(new PrioritizedListElement<T>(value, priority));
        Sort();
    }

    public void Remove(T value) {
        int index = list.FindIndex((x) => x.value == value);
        if (index != -1) list.RemoveAt(index);
        Sort();
    }

    public PrioritizedListElement<T> At(int index)
    {
        return list[index];
    }

    public T ValueAt(int index)
    {
        return At(index).value;
    }

    public int PriorityAt(int index)
    {
        return At(index).priority;
    }

    public bool ContainsValue(T value)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].value == value)
            {
                return true;
            }
        }

        return false;
    }

    public int FindValueIndex(T value)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].value == value)
            {
                return i;
            }
        }

        return -1;
    }

    public void UpdatePriority(T value, int priority)
    {
        int index = FindValueIndex(value);
        if (index == -1)
        {
            Add(value, priority);
        } else
        {
            list[index] = new PrioritizedListElement<T>(value, priority);
            Sort();
        }
    }

    private void Sort() {
        count = list.Count;
        list.Sort((a, b) => a.priority - b.priority);
    }

}
