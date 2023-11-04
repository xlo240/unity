using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPlayerRay : MonoBehaviour
{
    public Transform Pointer;

    // Update is called once per frame
    void LateUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit)){
            if(hit.collider.gameObject.GetComponent<XTerrianSelect>()){
                Pointer.position = hit.point;
            }
        }
        
    }
}
