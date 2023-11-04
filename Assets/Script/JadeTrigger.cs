using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JadeTrigger : MonoBehaviour
{
    public GameObject Jade;
    Animator jade_animator;

    void Start()
    {
        jade_animator = Jade.GetComponent<Animator>();
    }
    void OnTriggerEnter(Collider xcollider)
    {
        
        //xcollider2.gameObject.GetComponent<Voley>().voley_speed = 0;
        //xcollider2.gameObject.GetComponent<Voley>().AnimFuncStop();
        if (xcollider.name == "voley") {
            
            jade_animator.SetBool("trevoga_idle", true);
        }
    }
    void OnTriggerExit(Collider xcollider) {
        Debug.Log("Trigger Exit " + xcollider.name);
    }
    /*
    void Update()
    {
        
    }
    */
}
