using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainAmazon : MonoBehaviour
{
    [SerializeField] GameObject PrefVoley;
    [SerializeField] GameObject PrefJade;
    [SerializeField] GameObject PrefTarget2;
    public int selected_unit = 999; int predidush_selected_unit = 999;
    public Text info_txt; public Text info_txt2;
    GameObject selected_unitObj; GameObject deselected_unitObj;
    public GameObject arrowObj;
    float respound_coord_x = 1.8f; 
    float respound_coord2_x = -40.4f; float respound_coord2_z = -65.3f;
    
    int counter_id = 0; int counter_id2 = 0; string counter_id_txt = ""; string counter_id_txt2 = "";
    public float speed = 0; public Transform target; 
    public Transform hitPoint; public int clicked_ground = 0;
    float pred_curr_x = 0; float pred_curr_z = 0;
    float clicked_ground_x; float clicked_ground_y; float clicked_ground_z;
    Animator animator_hero;

    void Start()
    {
        
        InvokeRepeating("CustomUpdate", 0.5f, 0.5f);
        info_txt.text = "DDDD";

        //обращаемся к hero по умолчанию
        selected_unitObj = GameObject.Find("Voley_0");
        selected_unitObj.GetComponent<VoleyPrfScr>().select_deselect = true;
    }
    void CustomUpdate(){
        //Debug.Log("Selected unit id: Voley_" + selected_unit);
        info_txt.text = "Voley_" + selected_unit.ToString();
        if(selected_unit != predidush_selected_unit){//если выбран новый юнит
            
            string deselect_name_unit =  "Voley_" + predidush_selected_unit.ToString();
           // Debug.Log("deselect_name_unit: "+deselect_name_unit);
            info_txt2.text = deselect_name_unit;
            
            
            
            //speed = 0;
            //выбираем нового юнита
            selected_unitObj = GameObject.Find("Voley_" + selected_unit.ToString());
            selected_unitObj.GetComponent<VoleyPrfScr>().select_deselect = true;
            if(predidush_selected_unit != 999){
                deselected_unitObj = GameObject.Find(deselect_name_unit);
                deselected_unitObj.GetComponent<VoleyPrfScr>().select_deselect = false;
                 
            }

            float curr_x = selected_unitObj.transform.position.x;
            float curr_y = selected_unitObj.transform.position.y;
            float curr_z = selected_unitObj.transform.position.z;
            //Debug.Log("curr_x: "+curr_x + " curr_y: " + curr_y + " curr_z: " + curr_z);
            arrowObj.transform.position = new Vector3(curr_x, curr_y + 4, curr_z);
            target.transform.position = selected_unitObj.transform.position;//
            

           
            predidush_selected_unit = selected_unit;
        }
        
        
    }

   
    void FixedUpdate()
    {
        if(clicked_ground == 1){
            clicked_ground_x = hitPoint.transform.position.x;
            clicked_ground_y = hitPoint.transform.position.y;
            clicked_ground_z = hitPoint.transform.position.z;
            

            target.transform.position = new Vector3(clicked_ground_x, clicked_ground_y, clicked_ground_z);
            selected_unitObj.GetComponent<VoleyPrfScr>().speed = 0.1f;
            selected_unitObj.GetComponent<VoleyPrfScr>().stop_or_move = 1;
            selected_unitObj.GetComponent<VoleyPrfScr>().AnimFuncRun();//run
            //Debug.Log("clicked_ground = " + clicked_ground);
            clicked_ground = 0;
        }
        
       
        //Перемещение стрелки
        arrowObj.transform.position = new Vector3(selected_unitObj.transform.position.x, selected_unitObj.transform.position.y + 4, selected_unitObj.transform.position.z);
        
        
    }
//добавление юнитов на сцену
    public void Btn(){
        counter_id ++;
        counter_id_txt = counter_id.ToString();
        //Создается префаб
        GameObject Voley_tile = Instantiate(PrefVoley, transform.position, Quaternion.identity);
        Voley_tile.name = "Voley_"+counter_id_txt;
        respound_coord_x += 0.5f; 
        Voley_tile.transform.position = new Vector3(respound_coord_x, -0.3f, -77.7f);
       Voley_tile.GetComponent<VoleyPrfScr>().id_tile = counter_id; //передаем значение в публичную переменную в префаба
        
    }
    public void Btn2(){
       counter_id2 ++;
        counter_id_txt2 = counter_id2.ToString();
        GameObject Jade_tile = Instantiate(PrefJade, transform.position, Quaternion.identity);
        Jade_tile.name = "jade_" + counter_id_txt2;
        respound_coord2_x += 0.5f; 
        Jade_tile.transform.position = new Vector3(respound_coord2_x, 2f, respound_coord2_z);//задаем координаты появления
        //create target2
        GameObject target2_tile = Instantiate(PrefTarget2, transform.position, Quaternion.identity);
        target2_tile.name = "target2_" + counter_id_txt2;
        target2_tile.transform.position = new Vector3(respound_coord2_x + 1f, 2f, respound_coord2_z + 3f);

        Jade_tile.GetComponent<JadePrefSr>().id_tile = counter_id2; //передаем значение в публичную переменную в префаба
    }
}
