using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xTriggerPointer : MonoBehaviour
{
    void OnTriggerEnter(Collider xcollider2){
        
        xcollider2.gameObject.GetComponent<Voley>().voley_speed = 0;
        xcollider2.gameObject.GetComponent<Voley>().AnimFuncStop();
    }
}
