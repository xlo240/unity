using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xInputCamera : MonoBehaviour
{
    [SerializeField]
    public float _speed_Movement = 20f;
    [SerializeField]
    public float _speed_Rotation = 2f;

    private float rotY;
    private float speed = 10f;
    private Camera _camera;
    //public
    // Start is called before the first frame update
    void Start()
    {
         _camera = Camera.main;
    }

    
    void Update()
    {
        Input_Moouse();
    }
    private void Input_Moouse()
    {
        Movement();
        Rotation();
        GetInput();//клавиатура
    }

    

    private void Rotation()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            float _x_Cam = Input.GetAxis("Mouse X");
            float _y_Cam = Input.GetAxis("Mouse Y");

            float rotX = _camera.transform.localEulerAngles.y + _x_Cam * _speed_Rotation;
            //float rotX = _x_Cam;
            rotY += _y_Cam * _speed_Rotation;
            rotY = Mathf.Clamp(rotY, -90, 90);
            _camera.transform.localEulerAngles = new Vector3(-rotY, rotX, 0);
        }
    }
    private void Movement()
    {
        float mouse_Wheel = Input.GetAxis("Mouse ScrollWheel");

        if (mouse_Wheel > 0.1)
        {
            transform.localPosition += -transform.up * speed * Time.deltaTime; //вниз
            //_camera.transform.position += transform.forward * Time.deltaTime * _speed_Movement;//вперед
        }

        if(mouse_Wheel < -0.1)
        {
            transform.localPosition += transform.up * speed * Time.deltaTime; //вверх
            
            //_camera.transform.position -= transform.forward * Time.deltaTime * _speed_Movement;//назад
        }
    }
    private void GetInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.localPosition += transform.forward * speed * Time.deltaTime;
           // transform.localPosition += transform.up * speed * Time.deltaTime; //вверх
        }
        if (Input.GetKey(KeyCode.S))
        {
            //transform.localPosition += -transform.up * speed * Time.deltaTime; //вниз
            transform.localPosition += -transform.forward * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
           transform.localPosition += -transform.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.localPosition += transform.right * speed * Time.deltaTime;
        }
    }


}
