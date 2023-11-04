using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class xScrollLeft : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject xmainObj;
    void Start()
    {
        xmainObj = GameObject.Find("ScriptObject");
    }
    public void OnPointerEnter(PointerEventData eventData)

    {
        Debug.Log("LEFT");
        xmainObj.GetComponent<xmain>().left_scroll_speed = 0.1f;
    }
    public void OnPointerExit(PointerEventData eventData)

    {
        Debug.Log("LEFTout");
        xmainObj.GetComponent<xmain>().left_scroll_speed = 0;
    }


}
