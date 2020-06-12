using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSoldierStatue : MonoBehaviour {
    public GameObject soldierStatue;
    // Use this for initialization
    bool isHold = false;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            isHold = true;
            
        }else if (Input.GetMouseButtonUp(1))
        {
            isHold = false;
        }
        if (isHold == true) 
            soldierStatue.transform.Rotate(0, -1* Input.GetAxis("Mouse X"), 0);
        //euler ang,es


    }
}
