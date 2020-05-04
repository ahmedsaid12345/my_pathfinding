using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public float speed;
    Vector3[] path;
    //int targetindex;
    public Grid grid;
    public float maxdist;
    private void Awake()
    {
     //   grid = GetComponent<Grid>();
    }

    void Start()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);

    }

    // Update is called once per frame
    void Update()
    {
        // PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            PathRequestManager.RequestPath(transform.position, pos, OnPathFound);

        }

    }
    void OnPathFound(Vector3[] path, bool success)
    {
        if (success)
        {
            this.path = path;
            StopCoroutine("FellowPath");
            StartCoroutine("FellowPath");
        }

    }
    //just fellow path
    IEnumerator FellowPath()
    {
        Vector3 currentwaypoint = path[0];
        int targetindex=0;
        while (true)
        {
          
          //  if (transform.position == currentwaypoint )
               //if (transform.position == currentwaypoint+ new Vector3(1, 1, 0) * grid.NodeRaduis)
         if ( Vector3.Distance(  transform.position , currentwaypoint + new Vector3(1, 1, 0) * grid.NodeRaduis) <= maxdist)
                {
                targetindex++;
                print("pathlength"+path.Length +"target index "+targetindex);
                if (targetindex >= path.Length)
                {
                 //   print("targetindex"+targetindex);
                  //  print("pathlength"+path.Length);
                    yield break;
                }
                currentwaypoint = path[targetindex];
            }
             transform.position = Vector3.MoveTowards(transform.position, currentwaypoint+new Vector3(1,1,0)*grid.NodeRaduis, speed*Time.deltaTime);
        //    transform.position = Vector3.MoveTowards(transform.position, currentwaypoint, speed * Time.deltaTime);
            print("dist= "+ Vector3.Distance(transform.position, currentwaypoint + new Vector3(1, 1, 0) * grid.NodeRaduis));
            yield return null;
        }
    }


   /* public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetindex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);
                if (i==targetindex) { Gizmos.DrawLine(transform.position,path[i]); }
                else { Gizmos.DrawLine(path[i-1],path[i]); }
            }
        }

    }*/
}
