using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class LobbyManager : ServerManager
{
    #region Roomlist variable
    [SerializeField] Transform listContent_TR;
    #endregion

    #region Public Variable
    public bool isConnected = false;
    public RoomInfo roomInfo;
    public GameObject roomList_prefab;
    #endregion

    #region Private Variable
    private LobbyUI lobbyUI;
    #endregion

    #region LifeCycle
    private void Awake()
    {
        lobbyUI = GameObject.FindObjectOfType<LobbyUI>();
        PhotonNetwork.AutomaticallySyncScene = true;
        

        if(!PhotonNetwork.IsConnected)
        {
            ConnectedToServer();
            isConnected = true;
            Debug.Log("Lobby Entry completed");
        }
        else
        {
            ConnectedToServer();
            isConnected = true;
            Debug.Log("Lobby Reconnection Successful / " + "isConnected state : " + PhotonNetwork.NetworkClientState);
        }
    }
    #endregion

    #region Public Method
    public void CreateInit(float slider_value)
    {
        return;
    }

    /// <summary>
    /// 커스텀룸프로퍼티를 사용하여 방 생성
    /// </summary>
    /// <param name="roomName">LobbyUI에서 입력받는 Input값, 방이름</param>
    /// <param name="player">LobbyUI에서 Slider의 값을 받아오는 매개변수, 참가자 수</param>
    public void Create(string roomName, string nickName ,float player)
    {
        string room_name = roomName;
        string nick_name = nickName;

        if (string.IsNullOrEmpty(roomName) || string.IsNullOrEmpty(nickName))
        {
            return;
        }

        if (string.IsNullOrEmpty(room_name) || string.IsNullOrEmpty(nick_name))
        {
            roomName = "";
            nickName = "";
        }
        else
        {
            roomName = "";
            nickName = "";

            Hashtable roomHT = new Hashtable();
            roomHT.Add("RoomName", room_name);
            string[] roomList = new string[1];
            roomList[0] = "RoomName";

            RoomOptions roomOp = new RoomOptions();
            roomOp.MaxPlayers = (byte)player;
            roomOp.CustomRoomProperties = roomHT;
            roomOp.CustomRoomPropertiesForLobby = roomList;
            PhotonNetwork.CreateRoom(room_name, roomOp, null);

            Debug.Log(room_name + "Creating a Room");
            PhotonNetwork.LoadLevel("Room"); //Load Game Scene
        }
    }

    /// <summary>
    /// 방을 랜덤으로 참가하도록 하는 메서드 처리
    /// </summary>
    public void Quick_JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
        
    }

    /// <summary>
    /// 애플리케이션 종료
    /// </summary>
    /// 
   
    public void Exit_BTN()
    {
        //Application.Quit();
        PhotonNetwork.LeaveLobby();
        Debug.Log("Left Lobby");
        
        MultiplayerNetworkManager.Instance.Lobby.SetActive(false);
		MultiplayerNetworkManager.Instance.LoadingScreen.SetActive(false);
        MultiplayerNetworkManager.Instance.NetworkManager.SetActive(true);
    }
    #endregion

    #region Pun Method
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {   
        Debug.Log("Lobby access complete!");
        
    }

    /// <summary>
    /// 생성된 방을 리스트화 처리
    /// </summary>
    /// <param name="roomList"></param>
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(Transform tr in listContent_TR)
        {
            Destroy(tr.gameObject);
        }

        for (int i = 0; i < roomList.Count; i++)
        {
            roomInfo = roomList[i];
            if (roomInfo.PlayerCount == 0 || roomInfo.MaxPlayers == 0)
            {
                continue;
            }
            Instantiate(roomList_prefab, listContent_TR).GetComponent<RoomListInfo>().Set_RoomInfo(roomList[i]);
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        lobbyUI.Open_QuickStatePopUp();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {

    }
    #endregion

    List<RoomInfo> roomLists = new List<RoomInfo>();
    public void RefreshRoomList(){
        Debug.Log("Refreshing");
        OnRoomListUpdate(roomLists);

    }
}
