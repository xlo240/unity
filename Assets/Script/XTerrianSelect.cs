using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.EventSystems;
using UnityEngine.EventSystems;

public class XTerrianSelect : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnPointerClick(PointerEventData eventData) {
        GameObject getMainScript = GameObject.Find("ScriptObject");
        getMainScript.GetComponent<xmain>().clicked_ground = 1;//если кликнули по поверхности, запустить перемещение к точке
    }

}
