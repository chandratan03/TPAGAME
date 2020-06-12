using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneScript : MonoBehaviour {
    
    bool inZone = false;
    float speed = 2;
    float x= 0.0005f;
    float z= 0.0005f;
    public GameObject zone;
    bool zoneIsFinish = false;
    Character test;
    SphereCollider sCollider;
    
    void Start()
    {
        sCollider = GetComponent<SphereCollider>();
    }

    void Update() {
        if (zone.transform.localScale.x <= 0 && zone.transform.localScale.z <= 0)  
        {
            zoneIsFinish = true;
            zone.SetActive(false);
        }
        if(zoneIsFinish == false)
        {
            transform.localScale -= new Vector3(x, 0, z);
            sCollider.radius -= x;
        }
        //Debug.Log(transform.localScale);
        
        
        if(test != null)
        {
            Debug.Log(test.health);
        }
    }


    void OnTriggerExit(Collider temp)
    {
        //Debug.Log("found an boject");
        if (temp.transform.CompareTag("MainCharacter"))
        {
            Character soldier = temp.transform.GetComponent<Character>();
            Debug.Log("Gatcha exit");
            Debug.Log(soldier);
            soldier.inZone = false;
            if (soldier != null)
            {

                //test = soldier;
                soldier.inZone = false;
                soldier.waitOutSideZone = true;
            }



        }
    }
    void OnTriggerEnter(Collider temp)
    {
        if (temp.transform.CompareTag("MainCharacter"))
        {
            Debug.Log("Gatcha inside");
            Character soldier = temp.transform.GetComponent<Character>();
            Debug.Log(soldier);
            if (soldier != null)
            {

                soldier.inZone = true;
                soldier.waitOutSideZone = false;
            }



        }
    }
    IEnumerator DecreaseWait() {
        yield return new WaitForSeconds(2);
    }
}

