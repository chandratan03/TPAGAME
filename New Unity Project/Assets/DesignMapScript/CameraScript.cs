using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    float speedH = 2.0f;
    float speedV = 2.0f;
    float mSpeed = 1f;


    float hor = 0.00f;
    float ver = 0.00f;
    bool clicked = false;

    float yPosition;
    float zoom = 80;
    float maxZ = 60;
    float minZ = 40;
    float sensitivity = 3f;

    // Use this for initialization
    void Start () {
        
        yPosition = transform.position.y;   
	}
	
	// Update is called once per frame
	void Update () {
        ZoomingHandler();
        if (Input.GetMouseButtonDown(1))
        {
            clicked = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            clicked = false;
        }
        if(clicked == true)
        {
            ver -= speedH * Input.GetAxis("Mouse Y");
            hor += speedV * Input.GetAxis("Mouse X");
            ver = Mathf.Clamp(ver, -60f, 60f);
            //ver = Mathf.Clamp(ver, -60f, 60f);
            transform.eulerAngles = new Vector3(ver, hor, 0);
        }

        //transform.Translate(mSpeed * Input.GetAxis("Horizontal"), 0,mSpeed * Input.GetAxis("Vertical"), Space.Self);
        
        Vector3 front = transform.forward * mSpeed * Input.GetAxis("Vertical");
        front.y = 0;
        
        Vector3 sides = transform.right * Input.GetAxis("Horizontal") * mSpeed;

        if ((transform.position.z + front.z) < 95 & (transform.position.z + front.z) > -20 && (transform.position.x + front.x) < 100 && (transform.position.x + front.x) > -10)
        {
            transform.position += front;
        }


        if ((transform.position.x + sides.x) < 100 && (transform.position.x + sides.x) > -10 && (transform.position.z + sides.z) < 95 & (transform.position.z + sides.z) > -20)
        {
            transform.position += sides;

        }

     
        //transform.Translate()
        //Debug.Log(Input.GetAxis("Horizontal") + "   Horizontal" );
        //Debug.Log(Input.GetAxis("Vertical") + "   Vertical");

    }
    public void ZoomingHandler(){
        var FOView= Camera.main.fieldOfView;

        FOView -= Input.GetAxis("Mouse ScrollWheel") * 10 * sensitivity;
        //Debug.Log(FOView);
        //Debug.Log("Hello");
        FOView = Mathf.Clamp(FOView, minZ, maxZ);
        Camera.main.fieldOfView = FOView;
    }

}
