//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VoleyPrfScr : MonoBehaviour, IPointerClickHandler
{
    public int id_tile; 
    Animator voley_animator;
    public int state_of_battle = 1;
    public bool select_deselect = false;
    public int stop_or_move = 0;
    public bool fight_mode = false;
    public float speed = 0;
    CapsuleCollider VoleyCollider;
    Transform target; public int health_voley2 = 8;
    public int wrestle_top_bottom = 0; //1 - voley win / 2 - jade win
    public bool active_death = true;
    


    public void OnPointerClick(PointerEventData eventData){
                   
            //Обращаемся по клюку на юните к основному управляющему скрипту 
            GameObject getMainScript = GameObject.Find("ScriptObject");
            //и передаем туда его id
            getMainScript.GetComponent<MainAmazon>().selected_unit = id_tile;
       
        select_deselect = true;
    }
    
    void Start()
    {
        voley_animator = GetComponent<Animator>();
        VoleyCollider = GetComponent<CapsuleCollider>();
        target = GameObject.Find("TargetPoint").GetComponent<Transform>();
        InvokeRepeating("CustomUpdate", 0.1f, 0.5f);
        Debug.Log("health_voley from Voley " + health_voley2);
    }
     
    void FixedUpdate(){
        
        if(select_deselect){
            //Перемещение
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);

            //поворот в сторону движения
            Vector3 direction = target.position - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, 5 * Time.deltaTime, 0.0f);
            newDirection.y = 0;//чтоб не наклонялся юнит
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }


    string selector = "";
    void CustomUpdate(){
        Debug.Log("CustomUpdate__");
        if (!select_deselect){ //если  ne выбран
            speed = 0;
            voley_animator.SetBool("run2", false);
        }
        if(state_of_battle == 2 && stop_or_move == 0){
            Debug.Log("state_of_battle == 2 && stop_or_move == 0");
            CancelInvoke("CustomUpdate");
            InvokeRepeating("CustomUpdateFight", 0.1f, 0.6f);
        }
        VoleyCollider.radius = 0.15f;//уменьшаем размер коллайдера



    }
    void CustomUpdateFight(){
        Debug.Log("CustomUpdateFight__");
        if(state_of_battle == 2 && stop_or_move == 0){//****FIGHT MODE
            if(selector != "a"){//Предохранялка чтобы команда не запускалась каждую секунду
                //Debug.Log("Vole must fight");
                VoleyCollider.radius = 0.8f;//увеличиваем размер коллайдера чтобы отодвинуть соперника
                
                
                selector = "a";
            }
            Debug.Log("health_voley2!: " + health_voley2);
            voley_animator.SetBool("stoyka2", true);
        }
        else if(stop_or_move == 1){ //если убегае из файта
            CancelInvoke("CustomUpdateFight");
            InvokeRepeating("CustomUpdate", 0.1f, 0.5f);
            //VoleyCollider.radius = 0.15f;//уменьшаем размер коллайдера
            selector = "q";
            voley_animator.SetBool("stoyka2", false);
        }

        if(wrestle_top_bottom == 1) //voley win
        {//Переходим в не боевой режим
            
            CancelInvoke("CustomUpdateFight");
            InvokeRepeating("CustomUpdate", 0.1f, 0.5f);
            VoleyCollider.radius = 0.15f;
            state_of_battle = 1; selector = "";
            wrestle_top_bottom = 0; //обнуляем кто победил
            Debug.Log("voley win");
        } 
        
        if (health_voley2 < 1) //voley lose
        {
            CancelInvoke("CustomUpdateFight");
            state_of_battle = 1;
            voley_animator.SetBool("stoyka2", false);
            voley_animator.SetTrigger("drop2");
            //WrestleAnimTrigger("drop2");
            select_deselect = false;//деактивируем voley
            active_death = false;
            Debug.Log("AAAAAAAAAAAAAAAAA");
        }
        
    }
    
    public void WrestleAnimBool(string wrest_anim){
        //VoleyCollider.radius = 0.15f;
        voley_animator.SetBool(wrest_anim, true);        
    }
    public void WrestleAnimTrigger(string wrest_anim){
        if (wrest_anim == "drop2") {
            voley_animator.SetTrigger("drop2");
        } else if (wrest_anim == "idle_trevoga1")
        {
            voley_animator.SetBool("stoyka2", false);
            state_of_battle = 1;
            
            //CancelInvoke("CustomUpdateFight");
            //InvokeRepeating("CustomUpdate", 0.1f, 0.5f);
        }
        
    }




     void AnimFunc2(string name_animation){ //запускает анимацию триггеров из самого себя
        voley_animator.SetTrigger(name_animation);
    }
    
    public void AnimAttackOrBlock(int selector_animation){ //Передается из Jade
        if (active_death) {    
            if(stop_or_move == 0){
                //Attack or Block
                if(selector_animation == 1){ //attack
                    voley_animator.SetTrigger("udar_hand");
                } else if(selector_animation == 2){ //block
                    voley_animator.SetTrigger("block");
                }
            }
        }

    }
    public void AnimFuncRun(){ //from main script
        if(stop_or_move == 1){
            voley_animator.SetBool("run2", true);
        }
       
    }
    public void AnimFuncStop(){ //from TriigerPointer
        voley_animator.SetBool("run2", false);
    }
    
    void OnTriggerEnter(Collider xcollider){
        if( xcollider.gameObject.name == "TargetPoint"){
            AnimFuncStop();
            stop_or_move = 0;
            speed = 0;
            //Debug.Log("Trigger Voley " + xcollider.gameObject.name);
        }
        
   }
    
    
}
