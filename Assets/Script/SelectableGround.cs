using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableGround : MonoBehaviour, IPointerClickHandler
{
    void Start(){

    }
    
    public void OnPointerClick(PointerEventData eventData){
        //Debug.Log("Click Ground");
        GameObject getMainScript = GameObject.Find("ScriptObject");
        getMainScript.GetComponent<MainAmazon>().clicked_ground = 1;//если кликнули по поверхности, запустить перемещение к точке
    }
    
}
