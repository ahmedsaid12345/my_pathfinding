using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Grid : MonoBehaviour
{
    public bool displaygridGizmos;
    public Transform player;
    public LayerMask unWalkableMask;
    public float NodeRaduis;
   public GameObject spirit;
    public float offset;

    public Vector3 gridorigin;
    public Vector2 grideWorldSize;
    Node[,] grid;
 //   public float raisevalue;
    float Nodediameter;
    [HideInInspector]
    public int grideSizeX;
    [HideInInspector]
    public int grideSizeY;
    
    /// <summary>
    ///  for debuging
    /// </summary>
    TextMesh[,] debugtext;
    private void Awake()
    {
        Nodediameter = NodeRaduis * 2;
        grideSizeX = Mathf.RoundToInt(grideWorldSize.x / Nodediameter);
        grideSizeY = Mathf.RoundToInt(grideWorldSize.y / Nodediameter);
        CreateGrid();
      

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

            Node node = NodeFromWorldPoint(pos);

            if (node !=null) {
               
                node.gm.GetComponent<SpriteRenderer>().color = Color.green;

            }
      



         }





    }

    Vector3 worldbottomleft;
    void CreateGrid()
    {

        grid = new Node[grideSizeX, grideSizeY];
        worldbottomleft = transform.position +Vector3.one*offset - Vector3.right * grideWorldSize.x / 2 - Vector3.up * grideWorldSize.y / 2;
 
        for (int x = 0; x < grideSizeX; x++)
        {
            for (int y = 0; y < grideSizeY; y++)
            {
                GameObject gm_text = new GameObject("text",typeof(TextMesh));
                Vector3 WorldPoint = worldbottomleft + Vector3.right * (x * Nodediameter + NodeRaduis) + Vector3.up * (y * Nodediameter + NodeRaduis);
              
                bool walkable = !(Physics2D.OverlapCircle(WorldPoint,NodeRaduis,unWalkableMask));//.CheckSphere(WorldPoint, NodeRaduis, unWalkableMask));
                GameObject gm = Instantiate(spirit,WorldPoint+Vector3.one*NodeRaduis,Quaternion.identity);
                //  TextMesh text =new TextMesh();
                gm_text.GetComponent<TextMesh>().text = "0";
                gm_text.transform.position = WorldPoint + Vector3.one * NodeRaduis;
                gm_text.GetComponent<TextMesh>().fontSize = Mathf.FloorToInt(20);
                gm_text.GetComponent<TextMesh>().anchor = TextAnchor.MiddleCenter;
                //text.text = "0";

                grid[x, y] = new Node(walkable, WorldPoint, x, y,gm,gm_text.GetComponent<TextMesh>());

         

            }
        }
              // debugarray(this.grideSizeX,this.grideSizeY,Mathf.FloorToInt(Nodediameter*0.8f));
       // debugarray();




    }

    /// <summary>
    /// debugging array
    /// </summary>
    /// <param name="grideSizeX"></param>
    /// <param name="grideSizeY"></param>
    /// <param name="fontsize"></param>
    /// <returns></returns>
    void debugarray(int fontsize)
    {
        // TextMesh[,] debugtext = new TextMesh[grideSizeX,grideSizeY];

       // TextMesh text = new TextMesh();
                //  GameObject text = new GameObject("text" + (x * grideSizeY + y).ToString(), typeof(TextMesh));
             //   Node currnode = grid[x, y];
           //   text.text ="0" ;
               // currnode.text.gameObject.transform.position = currnode.worldposition + Vector3.one * NodeRaduis;
                //currnode.text.fontSize = fontsize;
               //currnode.text.anchor = TextAnchor.MiddleCenter;
                //debugtext[x, y] = currnode.text;

         
      //  return debugtext;
    }

    Vector3 getworldpoint(int x, int y)
    {
     
      
        Vector3 WorldPoint = worldbottomleft + Vector3.right * (x * Nodediameter + NodeRaduis) + Vector3.up * (y * Nodediameter + NodeRaduis);
       
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
        /* if (playerpos.x>=worldbottomleft.x && playerpos.y>=worldbottomleft.y && playerpos.x< (worldbottomleft.x+ grideWorldSize.x) && playerpos.y < (worldbottomleft.y + grideWorldSize.y)) {
            float percentX = (playerpos.x + grideWorldSize.x / 2) / grideWorldSize.x;
            float percentY = (playerpos.y + grideWorldSize.y / 2) / grideWorldSize.y;
            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);
            int x = Mathf.RoundToInt((grideSizeX - 1) * percentX);
            int y = Mathf.RoundToInt((grideSizeY - 1) * percentY);
            return grid[x, y];
        }
        return null;  */
        int cx = (int)((playerpos.x - worldbottomleft.x) * grideSizeX / ((grideSizeX - 1) * Nodediameter + NodeRaduis) - 1);
        int cy = (int)((playerpos.y - worldbottomleft.y) * grideSizeY / ((grideSizeY - 1) * Nodediameter + NodeRaduis) - 1);
        if (cx < 0 || cx >= grideSizeX || cy < 0 || cy >= grideSizeY) return null;
        return grid[cx, cy];
    }



    private void OnDrawGizmos()
    {
  
        if (displaygridGizmos) {
            for (int x = 0; x < grideSizeX; x++)
            {
                for (int y = 0; y < grideSizeY; y++)
                {
                    

                    Debug.DrawLine(getworldpoint(x, y), getworldpoint(x + 1, y));
                    Debug.DrawLine(getworldpoint(x, y), getworldpoint(x, y + 1));
                    if (grid[x, y].walkable == false)
                    {
                        Gizmos.DrawSphere(grid[x, y].worldposition, NodeRaduis);
                    }
                   

                }
            }
            Debug.DrawLine(getworldpoint(0, grideSizeY), getworldpoint(grideSizeX, grideSizeY));
            Debug.DrawLine(getworldpoint(grideSizeX, 0), getworldpoint(grideSizeX, grideSizeY));
        }
    }
}




