using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float speed;
    public float moveRange;
    private Vector3 oldPos;
    // Start is called before the first frame update
    void Start()
    {
        oldPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(transform.position.x - Time.deltaTime - speed , transform.position.y, transform.position.z);
        if (Vector3.Distance(oldPos,this.transform.position) >= moveRange)
        {
            this.transform.position = oldPos;
        }
    }
}
