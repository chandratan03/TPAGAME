using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    // Use this for initialization
    public float health = 100;
    public float shield = 0;
    public Animator animator;
    public bool inZone = true;
    public bool isDead = false;

    public bool waitOutSideZone = true;
	void Start () {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        //Debug.Log(inZone);
        checkIsInzone();
        
        if (health <= 0)
        {
            if(isDead == false)
            {
                isDead = true;
                die();

            }
        }
    }
	
    public void getShooted(float damage)
    {
        

        if(shield > 0)
        {
            shield -= damage;
        }else
        {
            health -= damage;

        }
        Debug.Log(health);
        if (health <= 0)
        {
            if (isDead == false)
            {
                isDead = true;
                die();

            }
        }
    }


    public void checkIsInzone()
    {
        if(inZone == false)
        {
            Debug.Log("HELLOo"+waitOutSideZone);
            if(waitOutSideZone == true)
            {
                Debug.Log(health);
                waitOutSideZone = false;
                StartCoroutine(OutSideZoneWait());
            }
        }
    }

    IEnumerator OutSideZoneWait()
    {
        yield return new WaitForSeconds(2);
        health -= 5;
        
        waitOutSideZone = true;
        if (health <= 0)
        {
            if (isDead == false)
            {
                isDead = true;
                die();

            }
        }
    }

    public void die()
    {
        animator.SetBool("isDie", true);
        GenerateBSP.playerCount--;
        StartCoroutine(waitDie());
        
    }

    
    IEnumerator waitDie()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

}


