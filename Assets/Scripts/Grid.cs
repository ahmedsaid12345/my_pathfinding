using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Grid : MonoBehaviour
{
    public bool displaygridGizmos;
    public Transform player;
    public LayerMask unWalkableMask;
    public float NodeRaduis;
    public GameObject cube;

    public Vector3 gridorigin;
    public Vector2 grideWorldSize;
    Node[,] grid;
    public float raisevalue;
    float Nodediameter;
    int grideSizeX;
    int grideSizeY;
    TextMesh[,] debugtext;
    private void Awake()
    {
        Nodediameter = NodeRaduis * 2;
        grideSizeX = Mathf.RoundToInt(grideWorldSize.x / Nodediameter);
        grideSizeY = Mathf.RoundToInt(grideWorldSize.y / Nodediameter);
        print("grideSizex="+grideSizeX+" gridesizey="+grideSizeY);
        debugtext = new TextMesh[grideSizeX, grideSizeY];
        CreateGrid();
      

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

            Node node = NodeFromWorldPoint(pos);

            //  Node highlightednode= NodeFromWorldPoint(pos);
            if (node !=null) {
                Debug.Log("u pressed at =" + (node.gridX * grideSizeY + node.gridY));
            }
            //}



        }

    }

    Vector3 worldbottomleft;
    void CreateGrid()
    {

        grid = new Node[grideSizeX, grideSizeY];
        worldbottomleft = transform.position - Vector3.right * grideWorldSize.x / 2 - Vector3.up * grideWorldSize.y / 2;
        print("origin"+worldbottomleft);
        for (int x = 0; x < grideSizeX; x++)
        {
            for (int y = 0; y < grideSizeY; y++)
            {
                Vector3 WorldPoint = worldbottomleft + Vector3.right * (x * Nodediameter + NodeRaduis) + Vector3.up * (y * Nodediameter + NodeRaduis);
           
                bool walkable = !(Physics.CheckSphere(WorldPoint, NodeRaduis, unWalkableMask));
                grid[x, y] = new Node(walkable, WorldPoint, x, y);
        

            }
        }
        for (int x = 0; x < grideSizeX; x++)
        {
            for (int y = 0; y < grideSizeY; y++)
            {
      
                GameObject text = new GameObject("text"+ (x * grideSizeY + y).ToString(),typeof(TextMesh));

               text.GetComponent<TextMesh>().text = (x * grideSizeY + y).ToString();
                text.transform.position = grid[x, y].worldposition + Vector3.one*NodeRaduis;
                text.GetComponent<TextMesh>().fontSize = 10;
                text.GetComponent<TextMesh>().anchor = TextAnchor.MiddleCenter;
                debugtext[x, y] = text.GetComponent<TextMesh>();

            }
        }





    }

    Vector3 getworldpoint(int x, int y)
    {
        //not complete
        //Vector3 worldbottomleft = transform.position - (Vector3.right * grideSizeX / 2) - (Vector3.up * grideSizeY / 2);
        Vector3 WorldPoint = worldbottomleft + Vector3.right * (x * Nodediameter + NodeRaduis) + Vector3.up * (y * Nodediameter + NodeRaduis);
        //    x *cell  + cellsize + y * cellsize;
        return WorldPoint;

    }

    public int MaxSize
    {
        get
        {
            return grideSizeX * grideSizeY;
        }

    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;
                if (checkX >= 0 && checkX < grideSizeX && checkY >= 0 && checkY < grideSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }

    /// <summary>
    /// //// take player position and back node where player stand
    /// </summary>
    public Node NodeFromWorldPoint(Vector3 playerpos)
    {
        if (playerpos.x>=worldbottomleft.x && playerpos.y>=worldbottomleft.y && playerpos.x< (worldbottomleft.x+ grideWorldSize.x) && playerpos.y < (worldbottomleft.y + grideWorldSize.y)) {
            float percentX = (playerpos.x + grideWorldSize.x / 2) / grideWorldSize.x;
            float percentY = (playerpos.y + grideWorldSize.y / 2) / grideWorldSize.y;
            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);
            int x = Mathf.RoundToInt((grideSizeX - 1) * percentX);
            int y = Mathf.RoundToInt((grideSizeY - 1) * percentY);
            return grid[x, y];
        }
        return null;
    }



    private void OnDrawGizmos()
    {
        // Gizmos.DrawWireCube(transform.position, new Vector3(grideWorldSize.x, 1, grideWorldSize.y));

        //  Gizmos.DrawLine();
        for (int x = 0; x < grideSizeX; x++)
        {
            for (int y = 0; y < grideSizeY; y++)
            {
                // Vector3 WorldPoint = worldbottomleft + Vector3.right * (x * Nodediameter + NodeRaduis) + Vector3.up * (y * Nodediameter + NodeRaduis);
                //   WorldPoint.y += raisevalue;
                //bool walkable = !(Physics.CheckSphere(WorldPoint, NodeRaduis, unWalkableMask));
                // grid[x, y] = new Node(walkable, WorldPoint, x, y);
              // Gizmos.DrawCube(grid[x, y].worldposition, Vector3.one*Nodediameter - (Vector3.one * .3f));

                Debug.DrawLine(getworldpoint(x, y), getworldpoint(x + 1, y));
                Debug.DrawLine(getworldpoint(x, y), getworldpoint(x, y + 1));
              
                // }
                //}


            }
        }
    }
}




