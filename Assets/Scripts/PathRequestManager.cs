using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathRequestManager : MonoBehaviour
{
    Queue<pathRequest> pathRequestQueue = new Queue<pathRequest>();
    static PathRequestManager Instance;
    PathFinding pathFinding;
    bool isprocessingpath;
    pathRequest currentpathrequest;
    private void Awake()
    {
        Instance = this;
        isprocessingpath = false;
        pathFinding = GetComponent<PathFinding>();
    }


    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[],bool> callback ) {
        pathRequest newrequest = new pathRequest(pathStart,pathEnd,callback);
        Instance.pathRequestQueue.Enqueue(newrequest);
        Instance.tryprocessnext();
    }

    private void tryprocessnext()
    {
        // throw new NotImplementedException();
        if (!isprocessingpath && pathRequestQueue.Count>0)
        {
            isprocessingpath = true;
            currentpathrequest = pathRequestQueue.Dequeue();
            pathFinding.startFindPath(currentpathrequest.pathStart,currentpathrequest.pathEnd);
        }


    }
    public void FinishedProcessingUnit(Vector3[] path,bool success)
    {
        currentpathrequest.callback(path,success);
        isprocessingpath = false;
        tryprocessnext();

    }

    struct pathRequest
    {
       public Vector3 pathStart;
        public Vector3 pathEnd;
       public Action<Vector3[],bool> callback;
        public pathRequest(Vector3 _pathStart, Vector3 _pathEnd, Action<Vector3[], bool> _callback)
        {
            pathStart = _pathStart;
            pathEnd = _pathEnd;
            callback = _callback;
        }
    }
}
