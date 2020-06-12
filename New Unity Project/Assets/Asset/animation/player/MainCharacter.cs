using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainCharacter : MonoBehaviour {

    // Use this for initialization
    public Character character;
    private Text health;
    private Text shield;
    private RawImage healthBar;
    private RawImage shieldBar;

    float maxHealth = 100;
    float maxShield = 100;
	void Start () {
        health = GameObject.Find("Health").GetComponent<Text>();
        shield = GameObject.Find("Shield").GetComponent<Text>();
        healthBar = GameObject.Find("HealthBar").GetComponent<RawImage>();
        shieldBar = GameObject.Find("ShieldBar").GetComponent<RawImage>();

        //character.health -= 90;
        //character.shield = 100;
    }


    // Update is called once per frame
    void Update () {
        float currHealth = character.health;
        float currShield = character.shield;

        health.text = character.health + "";
        shield.text = character.shield + "";
        healthBar.transform.localScale = new Vector3(currHealth/maxHealth * 3, 0.25f, 0);
        shieldBar.transform.localScale = new Vector3(currShield/maxShield * 3, 0.25f, 0);
	}
}
