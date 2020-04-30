using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node :IHeapItem<Node>
{
    public bool walkable;
    public Vector3 worldposition;
    public int G_Cost;
    public int H_Cost;

    public int gridX;
    public int gridY;
    int heapindex;

    public Node Parent;
    public Node(bool _walkable,Vector3 _worldpos,int _gridX,int _gridY) {
        this.walkable = _walkable;
        this.worldposition = _worldpos;
        this.gridX = _gridX;
        this.gridY = _gridY;

    }
    public int F_Cost
    {
        get
        {
            return G_Cost + H_Cost;
        }
    }
    public int HeapIndex
    {
        get { return heapindex; }
        set { heapindex= value; }
    }


    public int CompareTo(Node nodetocompare)
    {
        int Compare = F_Cost.CompareTo(nodetocompare.F_Cost);
        if (Compare == 0)
        {
            Compare = H_Cost.CompareTo(nodetocompare.H_Cost);
        }
        return Compare;
    }


}
