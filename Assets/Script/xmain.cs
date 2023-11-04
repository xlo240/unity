using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class xmain : MonoBehaviour
{
    public int clicked_ground = 0; public bool fight_mode = false;
    public Transform target; public Transform hitPoint;
    float clicked_ground_x; float clicked_ground_y; float clicked_ground_z;
    GameObject VoleyObject;
    GameObject xsphere; GameObject TestObj;
    public GameObject CmameraObj;
    public float top_scroll_speed = 0; public float bottom_scroll_speed = 0;
    public float left_scroll_speed = 0; public float right_scroll_speed = 0;

    void Start()
    {
        //selected_unitObj = GameObject.Find("Voley_0");
        VoleyObject = GameObject.Find("voley");
        TestObj = GameObject.Find("TestObj");
        xsphere = GameObject.Find("Sphere");
       
        
       // xcube.transform.position = new Vector3(3f, 1f, 43f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
           Debug.Log("AAAAAAAAAAA");
        }
        if(clicked_ground == 1 && fight_mode == false){//если кликнули по поверхности и не находимся в режиме файта
            
            clicked_ground_x = hitPoint.transform.position.x;
            clicked_ground_y = hitPoint.transform.position.y;
            clicked_ground_z = hitPoint.transform.position.z;
            target.transform.position = new Vector3(clicked_ground_x, clicked_ground_y, clicked_ground_z);

            VoleyObject.GetComponent<Voley>().voley_speed = 0.1f;
            VoleyObject.GetComponent<Voley>().AnimFuncRun();
            //selected_unitObj.GetComponent<VoleyPrfScr>().AnimFuncRun();//run
            clicked_ground = 0;
        }
        //скролл над местностью
        CmameraObj.transform.position = new Vector3(CmameraObj.transform.position.x, CmameraObj.transform.position.y, CmameraObj.transform.position.z + top_scroll_speed);
        CmameraObj.transform.position = new Vector3(CmameraObj.transform.position.x, CmameraObj.transform.position.y, CmameraObj.transform.position.z - bottom_scroll_speed);
        CmameraObj.transform.position = new Vector3(CmameraObj.transform.position.x - left_scroll_speed, CmameraObj.transform.position.y, CmameraObj.transform.position.z);
        CmameraObj.transform.position = new Vector3(CmameraObj.transform.position.x + right_scroll_speed, CmameraObj.transform.position.y, CmameraObj.transform.position.z);
    }
    public void Btn(){
        Debug.Log("BTN");

         
    }
}
