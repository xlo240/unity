using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger2 : MonoBehaviour
{
   public int state_of_battle = 0;
    void OnTriggerEnter(Collider xcollider){
        
        xcollider.gameObject.GetComponent<JadePrefSr>().state_of_trigger = "tru";
        xcollider.gameObject.GetComponent<JadePrefSr>().AnimFunc("tru");
        //Debug.Log("Jade Trigger " + xcollider.gameObject.name);
        
    }
    void OnTriggerExit(Collider xcollider){
        xcollider.gameObject.GetComponent<JadePrefSr>().state_of_trigger = "out";
        xcollider.gameObject.GetComponent<JadePrefSr>().AnimFuncMove("out");
    }
    
}
