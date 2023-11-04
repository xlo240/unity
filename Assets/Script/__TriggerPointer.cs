using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPointer : MonoBehaviour
{
    // Start is called before the first frame update
   void OnTriggerEnter(Collider xcollider){
    
    xcollider.gameObject.GetComponent<VoleyPrfScr>().AnimFuncStop();
    xcollider.gameObject.GetComponent<VoleyPrfScr>().stop_or_move = 0;
    xcollider.gameObject.GetComponent<VoleyPrfScr>().speed = 0;
   }
   
}
