using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{

    //public InputField nameInputFied;
	public Text buttonText;

    public GameObject Lobby;
    public GameObject LoadingScreen;

    void Start(){
        Lobby.SetActive(false);
        LoadingScreen.SetActive(true);
    }

	// Get's called when we click on connect button: set nickname to what we put in input field and connects to Photon Server
	public void OnClickConnectButton()
	{
		PhotonNetwork.NickName = PlayerPrefs.GetString("Username");
		PhotonNetwork.ConnectUsingSettings();
		buttonText.text = "Connecting...";
	}

	// Get's called when we connect to photon server: we want to join the lobby (the place where we can create and join rooms)
	public override void OnConnectedToMaster()
	{   
        //StreamChatBehaviour.instance.GetOrCreateClient(nameInputFied.text);
		PhotonNetwork.JoinLobby();
	}

	// Get's called when we join the lobby: we want to go to the lobby scene
	public override void OnJoinedLobby()
	{
		Lobby.SetActive(true);
        LoadingScreen.SetActive(false);
	}
}
