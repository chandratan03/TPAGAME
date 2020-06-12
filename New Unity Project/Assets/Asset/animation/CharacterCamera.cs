using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCamera : MonoBehaviour {

    // Use this for initialization
    float speed = 2;

    float ver;
    float hor;
    float sens = 10;
    public Transform target;
    int distance =5;
    float smoothness = 0.1f;
    Vector3 currRotation;
    Vector3 rotationSmoothVelo;
    Vector3 vect = new Vector3(0, 2, 0);
    void Start () {
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
        ver -= sens * Input.GetAxis("Mouse Y");
        hor += sens* Input.GetAxis("Mouse X");
        ver = Mathf.Clamp(ver, -20f, 30f); // p2 up, p3 down
        
        //ver = Mathf.Clamp(ver, -60f, 60f);
        currRotation = Vector3.SmoothDamp(currRotation, new Vector3(ver, hor, 0), ref rotationSmoothVelo, smoothness);
        transform.eulerAngles = currRotation;
        transform.position = (target.position+vect) - transform.forward * distance;
        
    }
    

}
