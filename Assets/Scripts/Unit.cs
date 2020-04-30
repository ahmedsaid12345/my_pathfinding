using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public float speed;
    Vector3[] path;
    int targetindex;
    void Start()
    {
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);

    }

    // Update is called once per frame
    void Update()
    {
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
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
    IEnumerator FellowPath()
    {
        Vector3 currentwaypoint = path[0];
        while (true)
        {
            if (transform.position == currentwaypoint)
            {
                targetindex++;
                if (targetindex >= path.Length)
                {
                    yield break;
                }
                currentwaypoint = path[targetindex];
            }
            transform.position = Vector3.MoveTowards(transform.position, currentwaypoint, speed*Time.deltaTime);
            yield return null;
        }
    }
    public void OnDrawGizmos()
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

    }
}
