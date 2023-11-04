using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xPointer : MonoBehaviour
{
    public Transform CameraTransform;
   public float Size;

    // Update is called once per frame
    void LateUpdate()
    {
        float scale = Vector3.Distance(transform.position, CameraTransform.position);
        transform.localScale = Vector3.one * scale * Size;
    }
}
