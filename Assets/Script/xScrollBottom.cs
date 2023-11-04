using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class xScrollBottom : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject xmainObj;
    void Start()
    {
        xmainObj = GameObject.Find("ScriptObject");
    }
    public void OnPointerEnter(PointerEventData eventData)

    {
        Debug.Log("Bottom");
        xmainObj.GetComponent<xmain>().bottom_scroll_speed = 0.1f;
    }
    public void OnPointerExit(PointerEventData eventData)

    {
        Debug.Log("Bottomout");
        xmainObj.GetComponent<xmain>().bottom_scroll_speed = 0;
    }

}
