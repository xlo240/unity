using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public Transform CameraTransform;
    public float Size;

    void LateUpdate()
    {
        float scale = Vector3.Distance(transform.position, CameraTransform.position);
        transform.localScale = Vector3.one * scale * Size;
    }
}
