using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class my_Test : MonoBehaviour
{
    PathFinding path;
    Vector3 startpos;
    Vector3 endpos;
    // Start is called before the first frame update
    private void Awake()
    {
        path = this.GetComponent<PathFinding>();
    }

    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            endpos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

        }
        if (Input.GetMouseButtonDown(1))
        {
            startpos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            path.startFindPath(startpos,endpos);
        }
    }
}
