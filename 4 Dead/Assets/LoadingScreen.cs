using System.Collections;

using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;


public class LoadingScreen : MonoBehaviourPunCallbacks
{
	public Text buttonText;

	public void JoinLobby()
    {
        StartCoroutine(OnClickConnectButton());
    }

    void Start(){
        MultiplayerNetworkManager.Instance.Lobby.SetActive(false);
		MultiplayerNetworkManager.Instance.LoadingScreen.SetActive(true);

		StartCoroutine(OnClickConnectButton());
    }

	IEnumerator OnClickConnectButton()
	{		
        yield return new WaitForSeconds(1.0f);
		PhotonNetwork.NickName = PlayerPrefs.GetString("Username");
		PhotonNetwork.ConnectUsingSettings();
		buttonText.text = "Creating Lobby...";
		MultiplayerNetworkManager.Instance.Lobby.SetActive(true);
		MultiplayerNetworkManager.Instance.LoadingScreen.SetActive(false);
	}

	public override void OnConnectedToMaster()
	{   
        //StreamChatBehaviour.instance.GetOrCreateClient(nameInputFied.text);
		PhotonNetwork.JoinLobby();
	}

	// public override void OnJoinedLobby()
	// {
	// 	MultiplayerNetworkManager.Instance.Lobby.SetActive(true);
	// 	MultiplayerNetworkManager.Instance.LoadingScreen.SetActive(false);
	// }

}
