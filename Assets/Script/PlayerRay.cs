using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRay : MonoBehaviour
{
    public Transform Pointer;

    void LateUpdate()
    {
            
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if(Physics.Raycast(ray, out hit)){
            if(hit.collider.gameObject.GetComponent<SelectableGround>()){
                Pointer.position = hit.point;
            }
        }
    }
}
