using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniMapScript : MonoBehaviour {

    // Use this for initialization
    public Transform target = null;
	void Start () {
        //Vector3 vect = new Vector3(target.transform.position.x, 0, target.transform.position.z);
        //transform.position = vect;


    }

    // Update is called once per frame
    void Update () {
        

         Vector3 vect = new Vector3(target.position.x, 10, target.position.z);
         transform.position = vect;
         

    }
    
}
