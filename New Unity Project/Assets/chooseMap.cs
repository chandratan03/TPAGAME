using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class chooseMap : MonoBehaviour {

    // Use this for initialization
    public static bool map1 = false;
    public static bool map2 = false;
    public static bool map3 = false;
    public static bool map4 = false;

    void Start () {
        map1 = false;
        map2 = false;
        map3 = false;
        map4 = false;

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void map1Btn()
    {
        map1 = true;
        SceneManager.LoadScene("play", LoadSceneMode.Single);
    }
    public void map2Btn()
    {
        map2 = true;
        SceneManager.LoadScene("play", LoadSceneMode.Single);

    }
    public void map3Btn()
    {
        map3 = true;
        SceneManager.LoadScene("play", LoadSceneMode.Single);

    }
    public void map4Btn()
    {
        map4 = true;
        SceneManager.LoadScene("play", LoadSceneMode.Single);
    }

}
