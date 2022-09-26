﻿
using UnityEngine;

public class Options : MonoBehaviour
{





    public GameObject OptionMenu;

    public GameObject OptionMenuDetails;

    public GameObject Bg;

    public GameObject exit;

    public GameObject returnButton;
    


    public void OpenOptions()
    {
        Bg.SetActive(true);
        exit.SetActive(true);
        OptionMenu.SetActive(true);
        OptionMenuDetails.SetActive(false);
        returnButton.SetActive(false);
    }

    public void ExitOptions()
    {
        Bg.SetActive(false);
        exit.SetActive(false);
        OptionMenu.SetActive(false);
        OptionMenuDetails.SetActive(false);
        returnButton.SetActive(false);
    }


    public void BackOptionMenu()
    {
        returnButton.SetActive(false);
        OptionMenuDetails.SetActive(false);
        OptionMenu.SetActive(true);
    }
    
    

    public void AboutMirrorJump()
    {
        OptionMenu.SetActive(false);
        OptionMenuDetails.SetActive(true);
        returnButton.SetActive(true);
    }

    public void OpenFeedback()
    {
        Application.OpenURL("https://app-staging.mirrorworld.fun/auth/login");
    }
    
    public void OpenFAQ()
    {
        Application.OpenURL("https://developer.mirrorworld.fun/#1696cb75-cb3b-46c0-9d3d-dea7c0f87f74");
    }
    
    public void OpenMirrorSDKLink()
    {
        Application.OpenURL("https://mirrorworld.fun/");
    }
    
   
}
