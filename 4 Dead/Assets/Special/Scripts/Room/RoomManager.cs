﻿
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public class RoomManager : ServerManager
{
    #region public variable
    public GameObject characters;
    public bool isMaster = false;

    public Transform RespawnSpot;
    public Transform EnterNameSpot;

    public GameObject PlayerUIAvatarChat;
    public GameObject PlayerAvatarName;
    #endregion

    [Header("RoomInfo UI")]
    [SerializeField] Text  roomName_TEXT;
    [SerializeField] Text playerCount_TEXT;
    [SerializeField] Text Instructions;
    [SerializeField] CanvasGroup readyButton;
    [SerializeField] RoomUI roomUI;

    public static RoomManager InstanceRoomManager;  

    [Header("Colors")]
    [SerializeField] Color DisabledColor;
    [SerializeField] Color EnabledColor;

    #region private variable
    private string nick_name;
    #endregion

    #region LifeCycle
    private void Awake()
    {
        roomUI = GameObject.FindObjectOfType<RoomUI>();
    }
    private void Start()
    {   
        InstanceRoomManager = this;
        if (PhotonNetwork.IsMasterClient)
        {
            isMaster = true;
        }

        RoomContent.SetActive(true);
        PlayerNumber.SetActive(false);
        GoHome.SetActive(false);
    }
    #endregion

    #region Pun Method
    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Name + "Successful room entry");
        PhotonNetwork.AutomaticallySyncScene = true;
        nick_name = PlayerPrefs.GetString("User_Name");

        Hashtable infoHT = PhotonNetwork.CurrentRoom.CustomProperties;
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { "Room", "room" } });
            Hashtable info = PhotonNetwork.LocalPlayer.CustomProperties;
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                PhotonNetwork.PlayerList[i].SetCustomProperties(new Hashtable {{"Room", "room"}});
            }
        }
        CreateCharacter();
        SetRoomInfo();
    }

    
    /// <param name="newPlayer"></param>
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerCount();
        
    }
  
    /// <param name="otherPlayer"></param>
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerCount();
      
        foreach (Transform item in RespawnSpot)
        {   
            Text[] texts = gameObject.GetComponentsInChildren<Text>();
            Debug.LogWarning(texts[0].text);
            Debug.LogWarning(gameObject.name);
            if(PlayerPrefs.GetString("OpponentName") == texts[0].text){
                Destroy(item.gameObject);
            } 
        }
        
        Debug.LogWarning("Deleted PlayerAvatarName");

    }
    #endregion

    void Delete(){
        
    }

    #region Private Method

    private void SetRoomInfo()
    {
        roomName_TEXT.text = string.Format(PhotonNetwork.CurrentRoom.Name + " Room");
        playerCount_TEXT.text = string.Format("Player : " + PhotonNetwork.CurrentRoom.PlayerCount +
                                                " / " + PhotonNetwork.CurrentRoom.MaxPlayers);
                                                CheckForPlayerCount();
    }

    private void UpdatePlayerCount()
    {
        playerCount_TEXT.text = string.Format("Player : " + PhotonNetwork.CurrentRoom.PlayerCount +
                                                " / " + PhotonNetwork.CurrentRoom.MaxPlayers);
                                                CheckForPlayerCount();
    }

     private void CreateCharacter()
    {
        if (characters == null)
        {
            Debug.Log("No characters have been created.");    
        }
        else
        {   
            GameObject PlayerCharacter = PhotonNetwork.Instantiate(characters.name, RespawnSpot.transform.position, Quaternion.identity);
        }
    }
    
    #endregion
    
    void CheckForPlayerCount(){
        if(PhotonNetwork.CurrentRoom.PlayerCount < PhotonNetwork.CurrentRoom.MaxPlayers){
            readyButton.interactable = false;
            readyButton.alpha = 0.6f;
            Instructions.text = "waiting for players to join ...";
            Instructions.color = DisabledColor;
            return;
        }

        readyButton.interactable = true;
        readyButton.alpha = 1f;
        Instructions.text = "click ready to start";
        Instructions.color = EnabledColor;          
    }

    public GameObject RoomContent;
    public GameObject PlayerNumber;
    public GameObject GoHome;
    public void NextScreen(){   //player declaring readyy

        RoomContent.SetActive(false);
        PlayerNumber.SetActive(true);

        GameObject PlayerAvatarName = Instantiate(this.PlayerAvatarName, EnterNameSpot);
        Text[] Nametexts = PlayerAvatarName.GetComponentsInChildren<Text>();
        Nametexts[0].text = PlayerPrefs.GetString("Username");

    }

    public void Menu(){
        AppManager.Instance.ChangeScene(AppManager.eSceneState.Menu);
    }

}


