using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class xScrollRight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject xmainObj;
    void Start()
    {
        xmainObj = GameObject.Find("ScriptObject");
    }

    public void OnPointerEnter(PointerEventData eventData)

    {
        
        xmainObj.GetComponent<xmain>().right_scroll_speed = 0.1f;
    }
    public void OnPointerExit(PointerEventData eventData)

    {
        xmainObj.GetComponent<xmain>().right_scroll_speed = 0;
    }
}
