using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


// this just Data Structure (Heap Data Structure)

public class Heap <T> where T :IHeapItem<T>
{

    T[] items;
    int CurrentItemCount=0;
    public Heap(int MaxHeapSize)
    {
        items = new T [MaxHeapSize];
    }

    /// <summary>
    
    private int GetLeftChildIndex(int elementIndex) => 2 * elementIndex + 1;
    private int GetRightChildIndex(int elementIndex) => 2 * elementIndex + 2;
    private int GetParentIndex(int elementIndex) => (elementIndex - 1) / 2;

    private bool HasLeftChild(int elementIndex) => GetLeftChildIndex(elementIndex) < CurrentItemCount;
    private bool HasRightChild(int elementIndex) => GetRightChildIndex(elementIndex) < CurrentItemCount;
    private bool IsRoot(int elementIndex) => elementIndex == 0;

    private T GetLeftChild(int elementIndex) => items[GetLeftChildIndex(elementIndex)];
    private T GetRightChild(int elementIndex) => items[GetRightChildIndex(elementIndex)];
    private T GetParent(int elementIndex) => items[GetParentIndex(elementIndex)];
    

    ////

    public void Add(T item) {
        item.HeapIndex = CurrentItemCount;
        items[CurrentItemCount]= item;
        SortUp(item);
        CurrentItemCount++;
    }
    public T RemoveFirst()
    {
       T FirstItem=  items[0];
        CurrentItemCount--;
        items[0]=items[CurrentItemCount];
        items[0].HeapIndex = 0;
        SortDown(items[0]);
        return FirstItem;

    }
    public bool Contains(T item)
    {
        return Equals(item,items[item.HeapIndex]);
    }


    public int Count
    {
        get {
            return CurrentItemCount;
        }
    }
    public void UpdateItem(T item) {
        SortUp(item);

    }

    void SortDown_other(T item)
    {
        
        while (HasLeftChild(item.HeapIndex))
        {
            var smallerIndex = GetLeftChildIndex(item.HeapIndex);
            if (HasRightChild(item.HeapIndex) && (GetRightChild(item.HeapIndex).CompareTo(GetLeftChild(item.HeapIndex)))<0)
                
            {
                smallerIndex = GetRightChildIndex(item.HeapIndex);
            }

            if (item.CompareTo(items[smallerIndex]) < 0)
            {
                break;
            }

            Swap(items[smallerIndex],item);
            // index = smallerIndex;
            item = items[smallerIndex];

        }

    }
    void SortDown(T item)
    {

        while (true)
        {
            int ChildIndexLeft =item.HeapIndex*2+1;
            int ChildIndexRight = item.HeapIndex * 2 + 2;
            int IndexSwap = 0;
            if (ChildIndexLeft<CurrentItemCount)
            {
                IndexSwap = ChildIndexLeft;
                if (ChildIndexRight<CurrentItemCount)
                {
                    if(items[ChildIndexLeft].CompareTo(items[ChildIndexRight])>0)
                    {
                        IndexSwap = ChildIndexRight;
                    }
                }
                if (item.CompareTo(items[IndexSwap])>0)
                {
                    Swap(item,items[IndexSwap]);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }

        }

    }


    void SortUp(T item)
    {
       T Parent_item= GetParent(item.HeapIndex);

        while (Parent_item.HeapIndex>=0 && item.CompareTo(Parent_item) < 0) //item is lower than parent_item
        {
            Swap(item,Parent_item);
            //Parent_item = GetParent(item.HeapIndex);
            Parent_item = items[item.HeapIndex];


        }
    }
    void SortUp_other(T item)
    {
        // T Parent_item = GetParent(item.HeapIndex);
        int parent_index = (item.HeapIndex - 1) / 2;
        while (true)
        {
            T parent_item = items[parent_index];
            if (item.CompareTo(parent_item) < 0)
            {
                Swap(item, parent_item);

            }
            else break;

        }
    }
    void Swap(T itemA,T itemB)
    {
         // var temp = itemA;
        //  itemA = itemB;
        // itemB = temp;
        items[itemA.HeapIndex] = itemB;
        items[itemB.HeapIndex] = itemA;
        var itemindex = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = itemindex;
    }
}
public interface IHeapItem<T>:IComparable<T>
{
    int HeapIndex
    {
        get;
        set;
    }


}
