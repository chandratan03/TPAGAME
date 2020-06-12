using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour {

	// Use this for initialization
    float speed = 7f;
    Animator animator;
    private Transform camera;
    float smoothness = 1.2f;
    public Character character;
    //public ParticleSystem muzzle;
	void Start () {
        animator = GetComponent<Animator>();
        camera = Camera.main.transform;

    }
	
	// Update is called once per frame
	void Update () {

        if (character.isDead == false)
        {
            animator.SetBool("isWalk", Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
           Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D));

            animator.SetBool("isFiring", Input.GetMouseButtonDown(0));
            Vector3 rotation = camera.eulerAngles;
            rotation.x = 0;
            rotation.z = 0;
            transform.eulerAngles = rotation;
            if (Input.GetKey(KeyCode.W))
            {
                Debug.Log("moving");


                transform.Translate(0, 0, speed * Time.deltaTime);

            }
            if (Input.GetKey(KeyCode.A))
            {
                Debug.Log("moving");
                transform.Translate(-speed * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey(KeyCode.S))
            {
                Debug.Log("moving");
                transform.Translate(0, 0, -speed * Time.deltaTime);

            }
            if (Input.GetKey(KeyCode.D))
            {
                Debug.Log("moving");

                transform.Translate(speed * Time.deltaTime, 0, 0);
            }
        }
        


        


    }
}
