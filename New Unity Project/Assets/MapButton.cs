using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MapButton : MonoBehaviour {

    // Use this for initialization

    const int size = 30;
    char[,,] map = new char[10, size + 5, size + 5];

    int boxSize = 3;
    bool[] mapIsAvailable = new bool[10];


    public Button defaultMapBtn;
    public Button map1Btn;
    public Button map2Btn;
    public Button map3Btn;
    public Button map4Btn;

    public GameObject map1GO;
    public GameObject map2GO;
    public GameObject map3GO;
    public GameObject map4GO;



    void readFile()
    {
        for (int i = 1; i <= 4; i++)
        {
            string path = path = "Assets/" + i + ".txt"; ;
            if (File.Exists(path) == false)
            {
                //Debug.Log(i);
                continue;
            }
            string[] lines = File.ReadAllLines(path);

            mapIsAvailable[i] = true;
            for (int j = 0; j < size; j++)
            {
               
                for (int k = 0; k < size; k++)
                {
                    //Debug.Log(lines[j][k]);
                    map[i, j, k] = lines[j][k];
                }
            }
        }


    }
    void activateButton()
    {
        if (mapIsAvailable[1] == true)
        {
            map1GO.SetActive(true);
        }
        if (mapIsAvailable[2] == true)
        {
            map2GO.SetActive(true);
        }
        if (mapIsAvailable[3] == true)
        {
            map3GO.SetActive(true);
        }

        if (mapIsAvailable[4] == true)
        {
            map4GO.SetActive(true);
        }
    }

    public void SelectDefault()
    {
        SceneManager.LoadScene("play", LoadSceneMode.Single);
    }

    public void init()
    {
        for (int i = 1; i <= 4; i++)
        {
            for (int j = 0; j < size; j++)
            {
                for (int k = 0; k < size; k++)
                {
                    map[i, j, k] = ' ';
                }
            }
        }

        for (int i = 0; i < 5; i++)
        {
            mapIsAvailable[i] = false;
        }
    }
    void Start () {
        init();
        readFile();
        activateButton();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
