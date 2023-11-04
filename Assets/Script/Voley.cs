using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Voley : MonoBehaviour
{
    Transform target;
    Animator v_animator;
    public float voley_speed = 0;
    GameObject CamObj;
    public float coord_x; public float coord_y; public float coord_z; public bool fight_mode = false;
    public string sopernik_name = ""; Transform sopernik; 
    void Start()
    {
        target = GameObject.Find("TargetPoint").GetComponent<Transform>();
        v_animator = GetComponent<Animator>();
        CamObj = GameObject.Find("CameraObj");
        //v_animator.SetBool("v_run", true);

    }

    int counter = 0; int counter2 = 0;
    void FixedUpdate()
    {
        if (fight_mode == false) //если режим бега
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, voley_speed);

            //поворот в сторону движения
            Vector3 direction = target.position - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, 5 * Time.deltaTime, 0.0f);
            newDirection.y = 0;//чтоб не наклонялся юнит
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
        else //если режим файта
        {
            target.transform.localPosition = new Vector3(coord_x, coord_y, coord_z);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, target.transform.localPosition, voley_speed);

            
            if (counter == 0)
            {
                sopernik = GameObject.Find(sopernik_name).GetComponent<Transform>();
                //Debug.Log(sopernik_name);
                //поворот в сторону движения
                StartCoroutine(EndOfTargetGrl());

                counter ++;
            }
            if (counter2 == 0)
            {
                Vector3 direction = sopernik.localPosition - transform.localPosition;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, 5 * Time.deltaTime, 0.0f);
                newDirection.y = 0;//чтоб не наклонялся юнит
                transform.rotation = Quaternion.LookRotation(newDirection);
            }
        }

    }

    IEnumerator EndOfTargetGrl()
    {
        yield return new WaitForSeconds(2f);
        counter2 = 2;
    }

    public void AnimFuncRun(){ //from main script
       
            v_animator.SetBool("v_run", true);
        //v_animator.SetTrigger("udar_hand");
         CamObj.GetComponent<xCamObjFollow>().cam_obj_speed = 0.1f;

    }
    public void AnimFuncStop(){ //from TriigerPointer
        v_animator.SetBool("v_run", false);
        CamObj.GetComponent<xCamObjFollow>().cam_obj_speed = 0.1f;
    }
}
