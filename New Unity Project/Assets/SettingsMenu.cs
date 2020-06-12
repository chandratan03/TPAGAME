using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingsMenu : MonoBehaviour
{

    // Use this for initialization

    private Resolution[] resolutions;
    public Dropdown dropDown;
    public Dropdown qualityDropDown;
    //public Text selectedGraphic;
    private Boolean vsync = true;

    private List<String> listOfResolution = new List<String>();
    private List<String> listOfQuality = new List<String>();

    private int currResIndex = 0;
    private int currQIndex = 0;

    private int frameRate;
    public Text fpsText;

    void Start()
    {
        initResolutions();
    }

    // Update is called once per frame
    void Update()
    {
        
        frameRate = (int)(1f/Time.unscaledDeltaTime);
        Debug.Log(frameRate);
        fpsText.text = frameRate.ToString();
       
    }


    public void initResolutions()
    {

        setResolution();
        setGraphic();

    }
    public void selectIndex()
    { // for select RESOLUTION
        currResIndex = dropDown.value;
        Debug.Log(currResIndex);
        Screen.SetResolution(resolutions[currResIndex].width, resolutions[currResIndex].height, true);

    }
    public void selectQuality()
    {
        currQIndex = qualityDropDown.value;
        Debug.Log(currQIndex);
        QualitySettings.SetQualityLevel(currQIndex, true);
    }

    public void setResolution()
    {
        resolutions = Screen.resolutions;
        foreach (Resolution res in resolutions)
        {
            listOfResolution.Add(res.width + "x" + res.height);
        }
        dropDown.AddOptions(listOfResolution);
        currResIndex = dropDown.options.Count - 1;
        Screen.SetResolution(resolutions[currResIndex].width, resolutions[currResIndex].height, true);
        dropDown.value = currResIndex;
    }

    public void setGraphic()
    {
        String[] temp = QualitySettings.names;
        foreach(String name in temp)
        {
            listOfQuality.Add(name);
        }
        qualityDropDown.AddOptions(listOfQuality);
        currQIndex = qualityDropDown.options.Count-1;
        QualitySettings.SetQualityLevel(currQIndex, true);
        qualityDropDown.value = currQIndex;
    }

    public void switchFPS()
    {
        if(vsync == true)
        {
            vsync = false;
            QualitySettings.vSyncCount = 0;
        }else if(vsync == false)
        {
            vsync = true;
            QualitySettings.vSyncCount = 1;
        }
    }

}
