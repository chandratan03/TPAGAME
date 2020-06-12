using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour {



    private Transform cam;
    public GameObject explosion;
    public float damage = 20;
    private bool ready = true;
    public ParticleSystem muzzleFlash;
	void Start () {
        cam = Camera.main.transform; 

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            
            if (ready == false) return;
            else
            {
                muzzleFlash.Play();

                shoot();
            }
        }
	}
    private void shoot()
    {
        //ready = false;
       
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            ready = false;
            Debug.Log(hit.transform.name);
            //var render = hit.transform.GetComponent<Renderer>();
            
            Character enemy = hit.transform.GetComponent<Character>();
            if(enemy != null)
            {
                enemy.getShooted(damage);
            }    
            GameObject a = Instantiate(explosion, hit.point, Quaternion.identity);
            Destroy(a, 1f);
            StartCoroutine(waitShoot());
            
        }
    }

    //IEnumerator startCol

    IEnumerator waitShoot()
    {
        yield return new WaitForSeconds(1);
        ready = true;
    }
}
