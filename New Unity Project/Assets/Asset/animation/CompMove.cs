using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompMove : MonoBehaviour {

    // Use this for initialization
    float speed = 7f;
    Animator animator;
    float smoothness = 1.2f;
    int direction;
    public Character character;
    public AutoShooting autoShoot;
    public bool readyToChange = true;
    void Start () {
        animator = GetComponent<Animator>();
        
    }
	
	// Update is called once per frame
	void Update () {

        if (character.isDead == false)
        {
            
            if (autoShoot.isShoot ==false)
            {
                animator.SetBool("isFiring", true);
                animator.SetBool("isWalk", false);
            }
            else
            {
                animator.SetBool("isFiring", false);
                animator.SetBool("isWalk", true);

                //Debug.Log("moving");

                if(readyToChange == true)
                {
                    readyToChange = false;
                    StartCoroutine(Direction());
                }
                transform.Translate(0, 0, speed * Time.deltaTime);

                
               
            }



            //animator.SetBool("isFiring", Input.GetMouseButtonDown(0));
            
        }






    }
    IEnumerator Direction()
    {
        yield return new WaitForSeconds(2);



        direction = Random.Range(0, 4);
        float rotate=0;
        switch (direction) {
            case 1:
                rotate = 180;
                break;
            case 2:
                rotate = 270;
                break;
            case 3:
                rotate = 360;
                break;
            case 0:
                rotate = 90;
                break;
        }


        transform.Rotate(new Vector3(0, rotate, 0));
        readyToChange = true;
    }

}
