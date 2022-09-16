using System.Collections;

using UnityEngine;

using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class LobbyUI : MonoBehaviour
{
    [Header("Button Container")]
    [SerializeField]GameObject buttonContainer;
    [SerializeField]GameObject IconContainer;
    [SerializeField]Button createRoom_BTN;
    [SerializeField]Button quick_BTN;
    [SerializeField]Button Exit_BTN;

    [Header("Room List")]
    [SerializeField]GameObject JoinUIPanel;

    [Header("Server Link TEXT")]
    [SerializeField]Text serverLink_TEXT;

    [Header("Create Room UI")]
    public GameObject createRoom_PopUp;
    [SerializeField]Slider playerCounter_SLIDER;
    [SerializeField]InputField roomName_INPUT;
    [SerializeField] Text nickName_INPUT;
    [SerializeField]Button create_BTN;
    [SerializeField]Button cancel_BTN;
    [SerializeField]Text playerCount_VALUE;

    [Header("Enter NickName UI")]
    //public GameObject enterNickName_PopUp;
    [SerializeField]Button nickName_Confirm_BTN;
    [SerializeField]Button nickName_Cancle_BTN;

    [SerializeField]Button viewrooms_BTN;
    [SerializeField]Button backfromviewrooms_BTN;
    Text enterNickName_INPUT;

    [SerializeField] GameObject quickState_PopUp;
    [SerializeField]LobbyManager lobbyManager;


    #region Private Variable
    private const string linking_TEXT = "Connecting To Server...";
    private const string linked_TEXT = "Connected To Server";
    private RoomInfo _roominfo;
    #endregion 

    #region LifeCycle
    private void Awake()
    {
        lobbyManager = GameObject.FindObjectOfType<LobbyManager>();
        Init();
        Create_Init();
    }

    private void Start()
    {

        if (lobbyManager.isConnected == true)
        {
            Invoke("LinkedServer", 1.5f);
            // 1.5초 후 LinkedServer메서드 실행
        }

        Exit_BTN.onClick.AddListener(lobbyManager.Exit_BTN);
        createRoom_BTN.onClick.AddListener(PopUp_On);
        create_BTN.onClick.AddListener(CreateRoom);
        quick_BTN.onClick.AddListener(QuickJoinRoom);
        cancel_BTN.onClick.AddListener(PopUp_Off);
        nickName_Confirm_BTN.onClick.AddListener(EnterRoom);
        nickName_Cancle_BTN.onClick.AddListener(Cancel_EnterRoom);
        viewrooms_BTN.onClick.AddListener(PopUp_On_Rooms);
        backfromviewrooms_BTN.onClick.AddListener(PopUp_Off_Rooms);
        playerCounter_SLIDER.onValueChanged.AddListener(delegate { SetPlayerCount(); });
    }
    #endregion

    #region Private Method
    /// <summary>
    /// 서버 연결 후 Lobby에 있는 UI 비활성화로 초기화
    /// </summary>
    private void Init()
    {
        serverLink_TEXT.text = linking_TEXT;
        //roomListContainer.SetActive(false);
        buttonContainer.SetActive(false);
    }

    private void LinkedServer()
    {
        serverLink_TEXT.text = linked_TEXT;
        //roomListContainer.SetActive(true);
        buttonContainer.SetActive(true);

    }
    private void PopUp_On()
    {
        createRoom_PopUp.SetActive(true);
        MultiplayerNetworkManager.Instance.Lobby.SetActive(false);
		MultiplayerNetworkManager.Instance.LoadingScreen.SetActive(false);
    }

    private void PopUp_On_Rooms()
    {
        JoinUIPanel.SetActive(true);
        MultiplayerNetworkManager.Instance.Lobby.SetActive(false);
		
    }

    private void PopUp_Off_Rooms()
    {
        JoinUIPanel.SetActive(false);
        MultiplayerNetworkManager.Instance.Lobby.SetActive(true);
		
    }

    private void PopUp_Off()
    {
        createRoom_PopUp.SetActive(false);
        MultiplayerNetworkManager.Instance.Lobby.SetActive(true);
    }

    /// <summary>
    /// 방 생성 팝업창 초기화
    /// </summary>
    private void Create_Init()
    {
        playerCounter_SLIDER.value = 2;
        playerCount_VALUE.text = "Player Count: " + playerCounter_SLIDER.value.ToString();
        roomName_INPUT.text = "";
        nickName_INPUT.text = PlayerPrefs.GetString("Username");
        lobbyManager.CreateInit(playerCounter_SLIDER.value);
    }

    /// <summary>
    /// 참가자 수를 조절하는 Slider의 value값을 text에 띄우도록 처리
    /// </summary>
    private void SetPlayerCount()
    {
        playerCount_VALUE.text = playerCounter_SLIDER.value.ToString();
    }

    /// <summary>
    /// createRoom_PopUp에서 닉네임 입력 후 PlayerPrefs로 저장 처리
    /// </summary>
    private void CreateRoom()
    {
        lobbyManager.Create(roomName_INPUT.text, nickName_INPUT.text, playerCounter_SLIDER.value);
        PhotonNetwork.NickName = nickName_INPUT.text;
        PlayerPrefs.SetString("Username", PhotonNetwork.NickName);
        Debug.Log(PlayerPrefs.HasKey("Username"));
    }

    /// <summary>
    /// enterRoom_PopUp에서 닉네임 입력 후 PlayerPrefs로 저장 처리
    /// </summary>
    private void EnterRoom()
    {
        _roominfo = lobbyManager.roomInfo;
        string room_Name = (string)_roominfo.CustomProperties["RoomName"];
        PhotonNetwork.NickName = enterNickName_INPUT.text;
        PlayerPrefs.SetString("Username", PhotonNetwork.NickName);
        PhotonNetwork.JoinRoom(room_Name);
        AppManager.Instance.ChangeScene(AppManager.eSceneState.Room);
    }

    private void Cancel_EnterRoom()
    {
        //enterNickName_INPUT.text = "";
        //enterNickName_PopUp.SetActive(false);
    }

    int temp = 0;
    [SerializeField] Text quickState_PopUpText;

    [SerializeField] Text QuickJoin_Debugger;
    [SerializeField] CanvasGroup QuickJoin_DebuggerCG;
  
    private void QuickJoinRoom()
    {   
        JoinUIPanel.SetActive(false);
        StartCoroutine("QuickJoinCounter");
        quickState_PopUp.SetActive(true);
        lobbyManager.Quick_JoinRoom();
        if(temp >= 5){
            quickState_PopUpText.text = "No Rooms available";
            print(temp);
        }
  
    }
    IEnumerator QuickJoinCounter(){
        temp = temp + 1;
        
        yield return temp;
    }

    void Update(){
       int roomCount = PhotonNetwork.CountOfRooms;
       int playersinRooms = PhotonNetwork.CountOfPlayersInRooms;
       if(roomCount > 0 || playersinRooms > 0){
            quick_BTN.interactable = true;
            QuickJoin_Debugger.text = "*Open Rooms are currently available";
            QuickJoin_DebuggerCG.interactable = true;
            
       }

       else{
            quick_BTN.interactable = false;
            QuickJoin_Debugger.text = "*There are currently no Open Rooms available";
            QuickJoin_DebuggerCG.interactable = false;

       }
    }
    // IEnumerator loader()
    // {
    //     load = 50;
    //     int temp = Random.Range(68, 96);
    //     while (load < temp)
    //     {
    //         load += Random.Range(0, temp - load);
    //         loadCounter.text = load.ToString() + "%";
    //         yield return new WaitForSeconds(3);
    //     }


    // }

    public void Close_QuickStatePopUp()
    {
        quickState_PopUp.SetActive(false);
        MenuManager.InstanceMenu.Multimenu.SetActive(true);
    }
    #endregion

    #region Public Method
    public void Open_QuickStatePopUp()
    {
        quickState_PopUp.SetActive(true);
        Invoke("Close_QuickStatePopUp", 2.0f);
        Invoke("PopUp_On", 2.0f);
    }
    #endregion
}
