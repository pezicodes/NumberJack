
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
    #endregion

    [Header("RoomInfo UI")]
    [SerializeField] Text  roomName_TEXT;
    [SerializeField] Text playerCount_TEXT;

    [SerializeField] RoomUI roomUI;

    public static RoomManager InstanceRoomManager;  

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
    }
    #endregion

    #region Private Method

    private void SetRoomInfo()
    {
        roomName_TEXT.text = string.Format("Room : " + PhotonNetwork.CurrentRoom.Name);
        playerCount_TEXT.text = string.Format("Player : " + PhotonNetwork.CurrentRoom.PlayerCount +
                                                " / " + PhotonNetwork.CurrentRoom.MaxPlayers);
    }

    private void UpdatePlayerCount()
    {
        playerCount_TEXT.text = string.Format("Player : " + PhotonNetwork.CurrentRoom.PlayerCount +
                                                " / " + PhotonNetwork.CurrentRoom.MaxPlayers);
    }

     private void CreateCharacter()
    {
        if (characters == null)
        {
            Debug.Log("No characters have been created.");    
        }
        else
        {   
            
            // GameObject go = Instantiate(characters, RespawnSpot);
            // Text[] texts = go.GetComponentsInChildren<Text>();
            // texts[0].text = PhotonNetwork.NickName; //name of string

    
            GameObject Go = PhotonNetwork.Instantiate(characters.name, RespawnSpot.transform.position, Quaternion.identity);
        

        }
    }

    
    #endregion
}
