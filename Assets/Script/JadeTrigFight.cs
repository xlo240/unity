using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using UnityEngine.Playables;

public class JadeTrigFight : MonoBehaviour
{
    GameObject Voley; GameObject ScriptObject;
    public GameObject Jade;
    Transform VoleyTransform; GameObject TargetHeroFight;
    //public PlayableDirector playableDirector;
    Animator VoleyAnimator; Animator JadeAnimator;
   

    void Start()
    {
        JadeAnimator = Jade.GetComponent<Animator>();
        //Debug.Log(Jade.name);
    }

    void OnTriggerEnter(Collider xcollider)
    {

        //xcollider2.gameObject.GetComponent<Voley>().voley_speed = 0;
        //xcollider2.gameObject.GetComponent<Voley>().AnimFuncStop();
        if (xcollider.name == "voley")
        {
            Voley = GameObject.Find(xcollider.name);
            Voley.GetComponent<Voley>().sopernik_name = Jade.name;
            VoleyAnimator = Voley.GetComponent<Animator>();
            TargetHeroFight = GameObject.Find("TargetPoint");
            JadeAnimator.SetBool("stoyka", true);
            VoleyAnimator.SetBool("stoyka", true);
            Debug.Log("FIGHT! " + xcollider.name);

            Voley.transform.parent = transform;//делаем hero дочерним объектом
            TargetHeroFight.transform.parent = transform;

            ScriptObject = GameObject.Find("ScriptObject");
            ScriptObject.GetComponent<xmain>().fight_mode = true;
            Voley.GetComponent<Voley>().fight_mode = true;
           
            Voley.GetComponent<Voley>().coord_x = -0.036f; Voley.GetComponent<Voley>().coord_y = -0.228f; Voley.GetComponent<Voley>().coord_z = -0.152f;
            Voley.GetComponent<Voley>().voley_speed = 0.01f;

           // Voley.transform.localPosition = new Vector3.MoveTowards(0, 0, 0);
           //hero_obj.transform.localPosition = Vector3.MoveTowards(hero_obj.transform.localPosition, target_hero.transform.localPosition, speed_hero);
           //CmameraObj.transform.position = new Vector3(CmameraObj.transform.position.x, CmameraObj.transform.position.y, CmameraObj.transform.position.z + top_scroll_speed);
           //target2.transform.position = new Vector3(new_x, curr_y_unit + 0.06f, new_z);
           //playableDirector.Play();
        }
    }
    /*
    void FixedUpdate()
    {
        Voley.transform.localPosition = Vector3.MoveTowards(Voley.transform.localPosition, TargetHeroFight.transform.localPosition, speed_hero);
    }*/
}
