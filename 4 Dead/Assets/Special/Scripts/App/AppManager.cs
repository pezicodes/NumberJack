using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using Photon.Pun;

public class AppManager : Singleton<AppManager>
{
   public enum eSceneState
    {
        App,
        Splash,
        omiN,
        Username,
        Menu,
        Room
    }
    public eSceneState sceneState;

    private void Start()
    {
        var app = GameObject.FindObjectOfType<AppManager>();
        if (app.GetInstanceID() != this.GetInstanceID())
        {
            DestroyImmediate(app.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        ChangeScene(eSceneState.Splash);
    }

    /// <summary>
    /// eSceneState에 따라 맞는 Scene으로 이동시켜주는 메서드
    /// </summary>
    /// <param name="sceneState">Scene의 종류 : App, Intro, Lobby, Room</param>
    /// 
    void Trash(){
        ChangeScene(eSceneState.Menu);    
    }
    public void ChangeScene(eSceneState sceneState)
    {
        switch(sceneState)
        {
            case eSceneState.App:
                {
                    SceneManager.LoadScene(eSceneState.App.ToString());
                }
                break;
            case eSceneState.omiN:
                {
                    //SceneManager.LoadScene(eSceneState.omiN.ToString());
                    SceneManager.LoadSceneAsync(eSceneState.Splash.ToString()).completed += (oper) =>
                        {
                            Invoke("Trash", 5);
                        };
                    
                    if (PhotonNetwork.IsConnected)
                    {
                        SceneManager.LoadSceneAsync(eSceneState.Splash.ToString()).completed += (oper) =>
                        {
                            ChangeScene(eSceneState.Menu);
                        };
                    }
                }
                break;
            case eSceneState.Username:
                {
                    if (PhotonNetwork.IsConnected)
                    {
                        PhotonNetwork.LoadLevel(eSceneState.Username.ToString());
                    }
                    else
                    {
                        SceneManager.LoadScene(eSceneState.Username.ToString());
                    }
                }
                break;
            
            case eSceneState.Menu:
                {
                    if (PhotonNetwork.IsConnected)
                    {
                        PhotonNetwork.LoadLevel(eSceneState.Menu.ToString());
                    }
                    else
                    {
                        SceneManager.LoadScene(eSceneState.Menu.ToString());
                    }
                }
                break;

            case eSceneState.Room:
                {
                    PhotonNetwork.LoadLevel(eSceneState.Room.ToString());
                }
                break;
        }
    }
}
