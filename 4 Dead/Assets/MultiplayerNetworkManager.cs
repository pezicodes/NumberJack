using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class MultiplayerNetworkManager : MonoBehaviour
{
    public static MultiplayerNetworkManager Instance;

    public GameObject NetworkManager;
    public GameObject LoadingScreen;
    public GameObject Lobby;
    
   public enum PanelStates
    {
        NetworkManager,
        LoadingScreen,
        Lobby,
        Game
    }

    public PanelStates multiplayerscreens;

    private void Start()
    {   
        Instance = this;
        var numjack = GameObject.FindObjectOfType<MultiplayerNetworkManager>();
        if (numjack.GetInstanceID() != this.GetInstanceID())
        {
            DestroyImmediate(numjack.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        //ChangeScene(PanelStates.LoadingScreen);
    }
    // public void ChangeScene(PanelStates multiplayerscreens)
    // {
    //     switch(multiplayerscreens)
    //     {
    //         case PanelStates.NetworkManager:
    //             {
    //                 NetworkManager.SetActive(true);
    //             } 
    //             break;
    //         case PanelStates.LoadingScreen:
    //             {
    //                 GameObject LoadingScreen = GameObject.FindGameObjectWithTag(PanelStates.LoadingScreen.ToString());
    //                 LoadingScreen.SetActive(true);
                    

    //                 if (PhotonNetwork.IsConnected)
    //                 {
                       
    //                     GameObject Lobby = GameObject.FindGameObjectWithTag(PanelStates.Lobby.ToString());
    //                     Lobby.SetActive(true);
    //                     LoadingScreen.SetActive(false);
                    
    //                 }
    //             }
    //             break;
    //         case PanelStates.Lobby:
    //             {
    //                 if (PhotonNetwork.IsConnected)
    //                 {
    //                    GameObject Lobby = GameObject.FindGameObjectWithTag(PanelStates.Lobby.ToString());
    //                    GameObject LoadingScreen = GameObject.FindGameObjectWithTag(PanelStates.LoadingScreen.ToString());
    //                     Lobby.SetActive(true);
  
    //                     LoadingScreen.SetActive(false);
    //                 }
    //                 else
    //                 {
    //                     GameObject Lobby = GameObject.FindGameObjectWithTag(PanelStates.Lobby.ToString());
    //                     GameObject LoadingScreen = GameObject.FindGameObjectWithTag(PanelStates.LoadingScreen.ToString());
    //                         Lobby.SetActive(true);
    //                         LoadingScreen.SetActive(false);
    //                 }
    //             }
    //             break;

    //         case PanelStates.Game:
    //             {
    //                 PhotonNetwork.LoadLevel(PanelStates.Game.ToString());
    //             }
    //             break;
    //     }
    // }


}

