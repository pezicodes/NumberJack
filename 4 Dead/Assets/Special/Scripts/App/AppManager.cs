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
        Intro,
        Lobby,
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
        ChangeScene(eSceneState.Intro);
    }

    /// <summary>
    /// eSceneState에 따라 맞는 Scene으로 이동시켜주는 메서드
    /// </summary>
    /// <param name="sceneState">Scene의 종류 : App, Intro, Lobby, Room</param>
    public void ChangeScene(eSceneState sceneState)
    {
        switch(sceneState)
        {
            case eSceneState.App:
                {
                    SceneManager.LoadScene(eSceneState.App.ToString());
                }
                break;
            case eSceneState.Intro:
                {
                    SceneManager.LoadScene(eSceneState.Intro.ToString());
                    if (PhotonNetwork.IsConnected)
                    {
                        SceneManager.LoadSceneAsync(eSceneState.Intro.ToString()).completed += (oper) =>
                        {
                            ChangeScene(eSceneState.Lobby);
                        };
                    }
                }
                break;
            case eSceneState.Lobby:
                {
                    if (PhotonNetwork.IsConnected)
                    {
                        PhotonNetwork.LoadLevel(eSceneState.Lobby.ToString());
                    }
                    else
                    {
                        SceneManager.LoadScene(eSceneState.Lobby.ToString());
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
