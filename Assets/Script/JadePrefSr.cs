using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JadePrefSr : MonoBehaviour
{
   public int id_tile = 0;
   Animator jade_animator;
   GameObject target2; float speed = 0; public string state_of_trigger = "tru";
   Transform target2Transform;
   GameObject[] playersArr; 
   int health_jade = 8; int health_voley;
   Rigidbody JadeRigidBody;

    void Start()
    {
        //Debug.Log("jade_ " + id_tile);
        //target2_
        jade_animator = GetComponent<Animator>();
        
        speed = 0.05f;
        target2 = GameObject.Find("target2_" + id_tile.ToString());

        target2Transform = target2.GetComponent<Transform>();
        InvokeRepeating("CustomUpdateIdle", 3f, 4f);
        //CancelInvoke("CustomUpdateIdle");
        //playersArr = GameObject.FindGameObjectsWithTag("Player"); //юнит ищет всех врагов
        //Debug.Log(playersArr[0]);
        //playersArr[0].GetComponent<Rigidbody>().mass = 100f;
    }
    
    void FixedUpdate(){
        //перемещение
        //transform.position = Vector3.MoveTowards(transform.position, target2.transform.position, speed); //тоже работает
        transform.position = Vector3.MoveTowards(transform.position, target2Transform.transform.position, speed);

        //поворот в сторону движения
        Vector3 direction = target2Transform.position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, 5 * Time.deltaTime, 0.0f);
        newDirection.y = 0;//чтоб не наклонялся юнит
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
    GameObject current_oponent;
    public int state_of_battle = 0; //0 - idlle 1-trevoga 2-fight
    
    void CustomUpdateIdle(){//логика движений
        
        playersArr = GameObject.FindGameObjectsWithTag("Player"); //юнит ищет всех врагов
        
       
        float curr_x = target2.transform.position.x;
        float curr_y = target2.transform.position.y;
        float curr_z = target2.transform.position.z;
        float curr_y_unit = transform.position.y;
        float new_x = RandomPos(curr_x);
        float new_z = RandomPos(curr_z);

        for(int i = 0; i < playersArr.Length; i++){
            
            float distance = Vector3.Distance(transform.position, playersArr[i].transform.position);
            //Debug.Log(distance);
            if(distance < 30f){
                if(playersArr[i].GetComponent<VoleyPrfScr>().active_death == true){//если это живой враг
                    state_of_battle = 1;
                    CancelInvoke("CustomUpdateIdle");
                    current_oponent = playersArr[i];
                    health_voley = current_oponent.GetComponent<VoleyPrfScr>().health_voley2;//Берем значение здоровья из voley
                    InvokeRepeating("CustomUpdateTrevoga", 0.5f, 1f);
                }
            }
        }
         
        jade_animator.SetBool("stoyka", false);
         jade_animator.SetBool("run", false);
        jade_animator.SetBool("walk", true);
        state_of_battle = 0;
        
        if(state_of_trigger == "tru"){
            speed = 0;
        }
        if(state_of_trigger == "out"){
            speed = 0.05f;
        }
        //выставляем позицию
        target2.transform.position = new Vector3(new_x, curr_y_unit + 0.06f, new_z); //выставляется позиция на врага
    
    
        
    }//end Custom Update
    void CustomUpdateTrevoga(){
        counterUpdFight = 0;
        float new_x = current_oponent.transform.position.x;
        float curr_y_unit = transform.position.y;
        float new_z = current_oponent.transform.position.z;
        //выставляем позицию
        
        target2.transform.position = new Vector3(new_x, curr_y_unit + 0.06f, new_z);

        float distance = Vector3.Distance(transform.position, current_oponent.transform.position);
        //Debug.Log("CustomUpdateTrevoga " + distance);
        if(distance < 2.3f){
            //Debug.Log("FIGHT MODE");
            CancelInvoke("CustomUpdateTrevoga");
            //Debug.Log("Cancel Invoke CustomUpdateTrevoga");
            state_of_battle = 2; speed = 0;
            //Передаем сопернику из пары о состоянии режима батла fight - 2
            current_oponent.GetComponent<VoleyPrfScr>().state_of_battle = state_of_battle;

            InvokeRepeating("CustomUpdateFight", 2f, 1f);//2
            
            jade_animator.SetBool("stoyka", true);
        }
        else {
            speed = 0.1f;
            jade_animator.SetBool("walk", false);
            jade_animator.SetBool("run", true);
            //Debug.Log("CustomUpdateTrevoga Run tru");
        }
    }

    // //************FIGHT MODE******************
    string[] animlist = new string[] {
        "udar_hand", "udar_foot"
    };
    int counter_animlist = 0;
    int counterUpdFight = 0;
    void CustomUpdateFight(){
        /*
        if(counter_animlist >= animlist.Length){
            counter_animlist = 0;
        }
        jade_animator.SetTrigger(animlist[counter_animlist]);
        counter_animlist ++;
        */
        if(counterUpdFight == 0){
            jade_animator.SetBool("run", false);
            jade_animator.SetBool("walk", false);
            jade_animator.SetBool("stoyka", true);
            counterUpdFight = 1;
        }
        //кто бьет, а кто защищается
        int[] who_is_win_turnArr = new int[2];
        who_is_win_turnArr = WhoIsWinInTurn();
        int hero_point = who_is_win_turnArr[0];
        int unit_point = who_is_win_turnArr[1];
        if(hero_point >= unit_point){ // hero atack unit block
            current_oponent.GetComponent<VoleyPrfScr>().AnimAttackOrBlock(1);//attack
            jade_animator.SetTrigger("block");
            health_jade -= 1;
            
        } else { // hero block unit attack
            current_oponent.GetComponent<VoleyPrfScr>().AnimAttackOrBlock(2);//block
            jade_animator.SetTrigger("udar_hand");
            current_oponent.GetComponent<VoleyPrfScr>().health_voley2 -= 1;//уменьшаем здоровье
            health_voley -= 1;
        }
       // Debug.Log("voley: " + health_voley + " jade: " + health_jade);
        //Who is Win
        
        if(health_jade <= 2){ //jade drop
            current_oponent.GetComponent<VoleyPrfScr>().wrestle_top_bottom = 1;
            health_jade = 1;
            //JadeRigidBody.GetComponent<Rigidbody>().isKinematic = true;
            StartCoroutine(IsKinematicTrue());
            current_oponent.GetComponent<VoleyPrfScr>().WrestleAnimTrigger("idle_trevoga1");
            jade_animator.SetTrigger("drop2");
            CancelInvoke("CustomUpdateFight");
            CancelInvoke("CustomUpdateTrevoga");
            Destroy(this);
            //InvokeRepeating("WrestleUpdate", 2f, 4f);
        }
        //если hero проиграл
        if (current_oponent.GetComponent<VoleyPrfScr>().active_death == false)
        {
            CancelInvoke("CustomUpdateFight");
            CancelInvoke("CustomUpdateTrevoga");
            InvokeRepeating("CustomUpdateIdle", 4f, 4f);
            //jade_animator.SetTrigger("idle_trevoga1");
            state_of_battle = 0; speed = 0;
        }
        /*
        else if (health_voley <= 2)  { //voley drop
            current_oponent.GetComponent<VoleyPrfScr>().wrestle_top_bottom = 2;
            health_voley = 1; state_of_battle = 0;
            current_oponent.GetComponent<VoleyPrfScr>().WrestleAnimTrigger("drop2");
            //jade_animator.SetTrigger("idle_trevoga1");
            CancelInvoke("CustomUpdateFight");
            CancelInvoke("CustomUpdateTrevoga");
            //InvokeRepeating("WrestleUpdate", 2f, 4f);
            current_oponent.GetComponent<VoleyPrfScr>().select_deselect = false;//деактивируем юнита
            current_oponent.GetComponent<VoleyPrfScr>().active_death = false;

            InvokeRepeating("CustomUpdateIdle", 0.5f, 4f);
        }
        */

        //если герой убегает из боя переходим в режим Trevoga
        float distance = Vector3.Distance(transform.position, current_oponent.transform.position);
        //Debug.Log("CustomUpdateFight " + distance);
        if(distance > 2.3f){//2.3
            //Передаем сопернику из пары о состоянии режима батла trevoga - 0
            current_oponent.GetComponent<VoleyPrfScr>().state_of_battle = 1;
            jade_animator.SetBool("stoyka", false);
            jade_animator.SetBool("run", true);
            state_of_battle = 1;

            CancelInvoke("CustomUpdateFight");

            InvokeRepeating("CustomUpdateTrevoga", 0.1f, 1f);
            
        }
    }

void WrestleUpdate(){
    speed = 0.01f;
    current_oponent.GetComponent<VoleyPrfScr>().select_deselect = false;
    if(health_jade == 1){ //Jade lose Voley win
        //jade_animator.SetBool("pin_bottom", true);
        current_oponent.GetComponent<VoleyPrfScr>().WrestleAnimBool("pin_top");
    } else if(health_voley == 1){ // Jade win voley lose
        jade_animator.SetBool("pin_top", true);
        current_oponent.GetComponent<VoleyPrfScr>().WrestleAnimBool("pin_bottom");
    }
}



    public void AnimFunc(string xstate){
       
       if(state_of_battle == 0){ // ************* IDE ****************
        if(state_of_trigger == "tru"){
           // jade_animator.SetTrigger(xstate);//
           jade_animator.SetBool("walk", false); //stop to idle_trevoga
            speed = 0;
        }
        else if(state_of_trigger == "out"){
            jade_animator.SetBool("walk", true);//going
            speed = 0.05f;
        }
        
       }
       else if(state_of_battle == 1){ // ************ TReVoga
            
            if(state_of_trigger == "tru"){
                jade_animator.SetBool("run", false);
                //Debug.Log("AnimFunc run false 164");

            } else if(state_of_trigger == "out"){
                jade_animator.SetBool("run", true);
                //Debug.Log("AnimFunc run tru 168");
            }
            
        }
        else if(state_of_battle == 2){
            jade_animator.SetTrigger(xstate);//
           
        }

       
       //jade_animator.SetTrigger(xstate);
       
       
    }
    public void AnimFuncMove(string move){
        
        if(state_of_battle == 0 && move =="out"){
            jade_animator.SetBool("run", false);
            jade_animator.SetBool("stoyka", false);
            jade_animator.SetBool("walk", true);
            speed = 0.05f;
            //Debug.Log("state_of_battle == 0 && state_of_trigger = out/move ==out");
        }
        else if(state_of_battle == 0 && move =="tru"){
            jade_animator.SetBool("walk", false);
            speed = 0;
        }
        else if(state_of_battle == 1 && move =="out"){
            jade_animator.SetBool("run", true);
            speed = 0.1f;
            //Debug.Log("AnimFuncMove run tru 187");
        }
        else if(state_of_battle == 2 && move =="tru"){
            jade_animator.SetBool("stoyka", true);
            speed = 0;
            
        }
    }



    
    void OnTriggerEnter(Collider xcollider){
        
        if("target2_" + id_tile.ToString() == xcollider.gameObject.name){
            state_of_trigger = "tru";
            AnimFunc("tru");
            //Debug.Log("Jade Trigger " + xcollider.gameObject.name);
        }
        
        
    }
    void OnTriggerExit(Collider xcollider){
         if("target2_" + id_tile.ToString() == xcollider.gameObject.name){
            state_of_trigger = "out";
            AnimFuncMove("out");
            //Debug.Log("Jade Trigger Exit " + xcollider.gameObject.name);
            
         }
        
    }
    int[] WhoIsWinInTurn(){
        int[] x = new int[2];
        x[0] = Random.Range(1, 50);
        x[1] = Random.Range(1, 50);
        return x;
    }



    float RandomPos(float coord_current){
        float pos1 = coord_current + 5f;
        float pos2 = coord_current - 5f;
        return Random.Range(pos1, pos2);
    }
    IEnumerator IsKinematicTrue(){
        yield return new WaitForSeconds(0.01f);
        JadeRigidBody.GetComponent<Rigidbody>().isKinematic = true;
    }



}
