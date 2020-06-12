using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potionScript : MonoBehaviour {

    // Use this for initialization

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.CompareTag("MainCharacter"))
        {
            Character soldier = col.transform.GetComponent<Character>();
            soldier.shield = 100;
            Destroy(gameObject);
        }
    }

}
