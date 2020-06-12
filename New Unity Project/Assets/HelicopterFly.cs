using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterFly : MonoBehaviour {

    // Use this for initialization
    Rigidbody rb;
    bool move = false;
    public Camera mainCamera;
    public Camera helicopterCamera;

    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if(move == true)
            transform.Translate(0, 0, 7f* Time.deltaTime);
    }

    public void Fly()
    {
        move = move ? false : true;
        mainCamera.enabled = false;
        helicopterCamera.enabled = true;
    }


}
