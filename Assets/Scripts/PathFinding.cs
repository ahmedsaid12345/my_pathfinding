using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class PathFinding : MonoBehaviour
{
    Grid grid;
   // PathRequestManager pathRequestManager;
    Vector3[] waypoints;
    Vector3 pathstart;

    public IEnumerator FindPath(Vector3 startpos,Vector3 targetpos)
    {
    
        Node StartNode = grid.NodeFromWorldPoint(startpos);
        Node TargetNode = grid.NodeFromWorldPoint(targetpos);
        waypoints = new Vector3[0];
        bool pathsuccess = false;

        if (StartNode.walkable && TargetNode.walkable)
        {

            Heap<Node> OpenSet = new Heap<Node>(grid.MaxSize);
            HashSet<Node> ClosedSet = new HashSet<Node>();
            OpenSet.Add(StartNode);
            while (OpenSet.Count > 0)
            {
                
                
               Node CurrentNode = OpenSet.RemoveFirst();
                ClosedSet.Add(CurrentNode);
                CurrentNode.gm.GetComponent<SpriteRenderer>().color = Color.green;
                CurrentNode.text.text = CurrentNode.F_Cost.ToString();
                if (CurrentNode == TargetNode)
                {
                    pathsuccess = true;
                    // TracePath(StartNode,TargetNode);

                    break;
                }
                foreach (Node neighbour in grid.GetNeighbours(CurrentNode))
                {
                    if (!(neighbour.walkable) || ClosedSet.Contains(neighbour)) continue;
                    int NewMovementcosttoNeighbour = CurrentNode.G_Cost + GetDistance(CurrentNode, neighbour);
                    if (NewMovementcosttoNeighbour < neighbour.G_Cost || !OpenSet.Contains(neighbour))
                    {
                        neighbour.G_Cost = NewMovementcosttoNeighbour;
                        neighbour.H_Cost = GetDistance(neighbour, TargetNode);
                        neighbour.Parent = CurrentNode;
                        neighbour.text.text = neighbour.F_Cost.ToString();
                        if (!OpenSet.Contains(neighbour))
                        {
                            neighbour.gm.GetComponent<SpriteRenderer>().color = Color.red;
                            OpenSet.Add(neighbour);
                         
                        }
                        else
                        {

                            OpenSet.UpdateItem(neighbour);
                            neighbour.text.text = neighbour.F_Cost.ToString();
                        }

                    }
                  //  yield return new WaitForSeconds(2f);
                }
               // yield return new WaitForSeconds(.5f);
            }
        }
        yield return null;
        if (pathsuccess)
        {
            waypoints = TracePath(StartNode,TargetNode);
          
            print("okey path found");
        }
        else
        {
            print("hey bitch no path");
        }
      //  pathRequestManager.FinishedProcessingUnit(waypoints,pathsuccess);
    }
    void drawpath()
    {
       
        Node currnode = grid.NodeFromWorldPoint(pathstart);
        for (int i=0;i<waypoints.Length;i++)
        {
          
            currnode.gm.GetComponent<SpriteRenderer>().color = Color.blue;
            Vector3 pos = waypoints[i];
            currnode = grid.NodeFromWorldPoint(pos);
        }
    }

     public void startFindPath(Vector3 pathStart, Vector3 pathEnd)
    {
        //throw new NotImplementedException();
        this.pathstart = pathStart;
        StartCoroutine(FindPath(pathStart,pathEnd));

    }

    Vector3[] TracePath(Node StartNode,Node EndNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = EndNode;
        while (currentNode != StartNode)
        {
            currentNode.gm.GetComponent<SpriteRenderer>().color = Color.blue;
           path.Add(currentNode);
           currentNode= currentNode.Parent;
        }
        currentNode.gm.GetComponent<SpriteRenderer>().color = Color.blue;
        //  waypoints= Simplifypath(path);
        // waypoints= path.ToArray();
        Array.Reverse(waypoints);
        return waypoints;
      
    }
    Vector3[] Simplifypath(List<Node> path)
    {
        List<Vector3> waypoint = new List<Vector3>();
        Vector2 olddirection = Vector2.zero;
        Vector2 newdirection = Vector2.zero;
        for (int i=1;i<path.Count;i++)
        {
            newdirection = new Vector2(path[i-1].gridX-path[i].gridX,path[i-1].gridY - path[i].gridY);
            if (olddirection!= newdirection)
            {
                waypoint.Add(path[i].worldposition);

            }
            olddirection = newdirection;
        }

        return waypoint.ToArray();
    }
    private void Awake()
    {
        grid = this.GetComponent<Grid>();
       // pathRequestManager = GetComponent<PathRequestManager>();
    }

    int GetDistance(Node nodeA,Node nodeB)
    {
        int distX = Mathf.Abs( nodeA.gridX-nodeB.gridX) ;
        int distY = Mathf.Abs(nodeA.gridY-nodeB.gridY);
        if (distX > distY)
            return 14 * distY + (distX - distY) * 10;
        return 14 * distX + (distY-distX)*10;

    }
    
    



}
