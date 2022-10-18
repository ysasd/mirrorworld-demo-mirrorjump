﻿
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{


    public ThemeManager ThemeManager;
    
    


    public GameObject LoginButton;

    private void Start()
    {
        SoundManager.Instance.GetAudioSource().clip = SoundManager.Instance.Clips[5];
        SoundManager.Instance.GetAudioSource().mute = SoundManager.Instance.GetSoundState();
        SoundManager.Instance.GetAudioSource().Play();
        LoginButton.SetActive(false);
        
        MirrorSDK.IsLoggedIn((result) =>
        {
            if (result)
            {
                MirrorSDK.StartLogin((LoginResponse)=>
                {
                
                    LoginState.HasLogin = true;
                    LoginState.Name = LoginResponse.user.username;
                    LoginState.WalletAddress= LoginResponse.user.wallet.sol_address;
                    LoginState.ID =  LoginResponse.user.id.ToString();

                    LoadingPanel.Instance.SetLoadingPanelEnable(true);
                    // 与服务器通信发送登陆信息
                    NetworkManager.Instance.SendUserBasicInfoReq(LoginState.WalletAddress);
                    //SceneManager.LoadScene("Menu");
                });
                
            
            }
            else
            {
                LoginButton.SetActive(true);
            }
        });

        
    }



    private bool IsDebug = false;

    private void Awake()
    {
        EventDispatcher.Instance.userInfoDataReceived += OnUserDataReceived;
    }

    private void OnDestroy()
    {
        EventDispatcher.Instance.userInfoDataReceived -= OnUserDataReceived;
    }

    private void OnUserDataReceived(UserInfoData userInfoData)
    {    
        
        // 数据记录
        // 处理场景
        // 先清除所有的场景记录，避免换号登陆之后场景还是解锁状态
        PlayerPrefs.SetInt(Constant.Theme_Pre + Constant.ThemeDesertIndex, 0);
        PlayerPrefs.SetInt(Constant.Theme_Pre + Constant.ThemeSnowIndex, 0);
        PlayerPrefs.SetInt(Constant.Theme_Pre + Constant.ThemeCyberpunkIndex, 0);
        PlayerPrefs.SetInt(Constant.Theme_Pre + Constant.ThemePastureIndex, 0);

        foreach(var scenes in userInfoData.scenes)
        {
            if(scenes.scene_id == 0)
            {
                continue;
            }

            PlayerPrefs.SetInt(Constant.Theme_Pre + scenes.scene_id, 1);
        }

        // 处理NFT
        LoginState.defaultRoleData = null;
        LoginState.mintableRoleData = null;
        foreach(var data in userInfoData.packages)
        {
            if (data.is_default)
            {
                LoginState.defaultRoleData = data;
            }
            else
            {
                LoginState.mintableRoleData = data;
            }
        }

        // 检查PlayerPref中的当前nft是否还存在
        // TODO
        LoadingPanel.Instance.SetLoadingPanelEnable(false);

        PlayerPrefs.SetString("HasReceiveToken", userInfoData.airdrop_sol? "true":"false");
        
        
        // 处理 token guidence 流程
        if (userInfoData.airdrop_sol)
        {
            PlayerPrefs.SetString("HasReceiveToken", "true");      
        }
      

        SceneManager.LoadScene("Menu");
    }

    public void OnDebugClick()
    {   
        
        
        Debug.LogWarning("click");
        IsDebug = true;
        
    }
    
    public void PlayGame()
    {    
        if (ThemeManager.GetCurrentLockState())
        {   
            // add some tips 
            return;
        }
        
        SoundManager.Instance.PlaySound(SoundName.Button);
        
        SoundManager.Instance.GetAudioSource().mute = true;
        SoundManager.Instance.GetAudioSource().clip = null;
        
        SoundManager.Instance.GetAudioSource().mute = SoundManager.Instance.GetSoundState();
        
        SceneManager.LoadScene("Game");
    }

    public void Login()
    {
        
       

        if (IsDebug)
        {
            SoundManager.Instance.PlaySound(SoundName.Button);
            SceneManager.LoadScene("Menu");
        }
        else
        {   
            
            SoundManager.Instance.PlaySound(SoundName.Button);

         
            MirrorSDK.StartLogin((LoginResponse)=>
            {
                
                LoginState.HasLogin = true;
                LoginState.Name = LoginResponse.user.username;
                LoginState.WalletAddress= LoginResponse.user.wallet.sol_address;
                LoginState.ID =  LoginResponse.user.id.ToString();

                LoadingPanel.Instance.SetLoadingPanelEnable(true);
                // 与服务器通信发送登陆信息
                NetworkManager.Instance.SendUserBasicInfoReq(LoginState.WalletAddress);
                //SceneManager.LoadScene("Menu");
            });
        }
        
       
    }

    public void OpenWallet()
    {   
        SoundManager.Instance.PlaySound(SoundName.Button);
        MirrorSDK.OpenWalletPage();
    }
    
    public void OpenMarket()
    {   
        SoundManager.Instance.PlaySound(SoundName.Button);
        MirrorSDK.OpenMarketPage();
    }

    public void ClearAllPersistingData()
    {
        PlayerPrefs.DeleteAll();
    }


    
}
