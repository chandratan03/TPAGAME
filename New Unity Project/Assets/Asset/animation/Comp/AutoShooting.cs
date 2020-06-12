using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShooting : MonoBehaviour {

    // Use this for initialization
    public GameObject explosion;
    public float damage = 10;
    private bool ready = true;
    public GameObject gunObject;
    public Character character;
    public bool isShoot = false;
    public ParticleSystem muzzleFlash;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
         
        if (ready == false) return;
        else
        {
            if (character.isDead == true) return;
            shoot();
         }
        
    }
    private void shoot()
    {
        //ready = false;
        isShoot = true;
        RaycastHit hit;
        if (Physics.Raycast(gunObject.transform.position, gunObject.transform.forward, out hit, 20f)) // 20 size nya ktoak
        {
            
            Transform hitted = hit.transform;
            if (!hitted.CompareTag("MainCharacter")) return;
            
            ready = false;
            Debug.Log(hit.transform.name);
            //var render = hit.transform.GetComponent<Renderer>();

            Character enemy = hit.transform.GetComponent<Character>();



            if (enemy != null)
            {
                enemy.getShooted(damage);
            }
            muzzleFlash.Play();
            GameObject a = Instantiate(explosion, hit.point, Quaternion.identity);
            Destroy(a, 1f);
            StartCoroutine(waitShoot());

        }
    }

    //IEnumerator startCol

    IEnumerator waitShoot()
    {
        yield return new WaitForSeconds(2);
        ready = true;
        isShoot = false;
    }
}
