using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class xScrollTop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler

{

    GameObject xmainObj;
    void Start()
    {
        xmainObj = GameObject.Find("ScriptObject");
    }

    public void OnPointerEnter(PointerEventData eventData)

    {
        Debug.Log("TOP");
        xmainObj.GetComponent<xmain>().top_scroll_speed = 0.1f;
    }
    public void OnPointerExit(PointerEventData eventData)

    {
        Debug.Log("TOPout");
        xmainObj.GetComponent<xmain>().top_scroll_speed = 0;
    }
}
