using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
public class MainMenu : MonoBehaviour {

    // Use this for initialization
    Rigidbody rb;

    const int size = 30; 
    char[,,] map = new char[10,size + 5, size + 5];

    int boxSize = 3;
   


    
    

    void Start () {
     
      
	}
	
	// Update is called once per frame
	void Update () {
      
        
	}
    
    public void goToDesignMap()
    {
        SceneManager.LoadScene("DesignMapScene", LoadSceneMode.Single);
    }
   
 
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
        
    }

    
    public void goToDefaultMap()
    {
        SceneManager.LoadScene("play", LoadSceneMode.Single);
    }

}
