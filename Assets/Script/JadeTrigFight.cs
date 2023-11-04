using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
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
            TargetHeroFight = GameObject.Find("FightTarget");
            JadeAnimator.SetBool("stoyka", true);
            VoleyAnimator.SetBool("strafe_left", true);
            //Debug.Log("FIGHT! " + xcollider.name);

            Voley.transform.parent = transform;//делаем hero дочерним объектом
            TargetHeroFight.transform.parent = transform;

            ScriptObject = GameObject.Find("ScriptObject");
            ScriptObject.GetComponent<xmain>().fight_mode = true;
            Voley.GetComponent<Voley>().fight_mode = true;
           
            Voley.GetComponent<Voley>().coord_x = -0.165f; Voley.GetComponent<Voley>().coord_y = -0.75f; Voley.GetComponent<Voley>().coord_z = -0.3f;
            Voley.GetComponent<Voley>().voley_speed = 0.01f;

            //playableDirector.Play();
            StartCoroutine(EndOfStrafe());
        }
    }
    IEnumerator EndOfStrafe()
    {
        yield return new WaitForSeconds(1f);
        VoleyAnimator.SetBool("strafe_left", false);
        VoleyAnimator.SetBool("stoyka", true);
        //FightBtn.SetActive(true);
    }
    int[] points_arr = new int[10];
    public void StartFight(int[] points)//Запускается из Voley.cs
    {
        Debug.Log("StartFight");
        points_arr = points;
        InvokeRepeating("FightTurn", 0.1f, 1f);
    }
    int counter_fight_turn = 0;
    
    void FightTurn()
    {
        //Debug.Log("voley_speed" + Voley.GetComponent<Voley>().voley_speed);
        counter_fight_turn++;
        if (counter_fight_turn == 1)
        {
            if (points_arr[0] <= points_arr[5])
            {
                //jade attack hero block
                Debug.Log("jade attack | hero block");
                JadeAttackHeroBlock();
            }
            else
            {
                //jade block  hero attack
                Debug.Log("jade block | hero attack");
                JadeBlockHeroAttack();
            }
        } else if (counter_fight_turn == 2)
        {
            if (points_arr[1] <= points_arr[6])
            {
                //jade attack hero block
                Debug.Log("jade attack | hero block");
                JadeAttackHeroBlock();
            }
            else
            {
                //jade block  hero attack
                Debug.Log("jade block | hero attack");
                JadeBlockHeroAttack();
            }
        } 
        else if (counter_fight_turn == 3)
        {
            if (points_arr[2] <= points_arr[7])
            {
                //jade attack hero block
                Debug.Log("jade attack | hero block");
                JadeAttackHeroBlock();
            }
            else
            {
                //jade block  hero attack
                Debug.Log("jade block | hero attack");
                JadeBlockHeroAttack();
            }
        }
        else if (counter_fight_turn == 4)
        {
            if (points_arr[3] <= points_arr[8])
            {
                //jade attack hero block
                Debug.Log("jade attack | hero block");
                JadeAttackHeroBlock();
            }
            else
            {
                //jade block  hero attack
                Debug.Log("jade block | hero attack");
                JadeBlockHeroAttack();
            }
        }
        else if (counter_fight_turn == 5)
        {
            if (points_arr[4] <= points_arr[9])
            {
                //jade attack hero block
                Debug.Log("jade attack | hero block");
                JadeAttackHeroBlock();
            }
            else
            {
                //jade block  hero attack
                Debug.Log("jade block | hero attack");
                JadeBlockHeroAttack();
            }
        }
        
    }
    void JadeAttackHeroBlock()
    {
        JadeAnimator.SetTrigger("udar_hand");
        VoleyAnimator.SetTrigger("block");
     
    }
    void JadeBlockHeroAttack()
    {
        JadeAnimator.SetTrigger("block");
        VoleyAnimator.SetTrigger("udar_hand");
    }

}
