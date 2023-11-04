using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xCamObjFollow : MonoBehaviour
{
    Transform target;
    public float cam_obj_speed = 0;
    public Vector3 offset;
    void Start()
    {
        target = GameObject.Find("voley").GetComponent<Transform>();
    }

    
    void FixedUpdate()
    {
        
        transform.position = target.position + offset;
        
    }
}
