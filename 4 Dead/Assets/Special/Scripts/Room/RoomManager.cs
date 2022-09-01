
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public class RoomManager : ServerManager
{
    #region public variable
    //public GameObject characters;
    public bool isMaster = false;
    #endregion

    [Header("RoomInfo UI")]
    [SerializeField] Text  roomName_TEXT;
    [SerializeField] Text playerCount_TEXT;

    [SerializeField] RoomUI roomUI;

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
        if (PhotonNetwork.IsMasterClient)
        {
            isMaster = true;
        }
    }
    #endregion

    #region Pun Method
    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Name + "방 입장 성공");
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
        //CreateCharacter();
        SetRoomInfo();
    }

    /// <summary>
    /// 새로운 player가 방에 입장할 때 채팅창에 플레이어 입장 소식을 처리
    /// </summary>
    /// <param name="newPlayer"></param>
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerCount();
    }

    /// <summary>
    /// player가 방에서 나갈 때 채팅창에 플레이어 퇴장 소식을 처리
    /// </summary>
    /// <param name="otherPlayer"></param>
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerCount();
    }
    #endregion

    #region Private Method
    /// <summary>
    /// 방생성 시 입력한 방이름, 닉네임, 참가자 수를 세팅
    /// </summary>
    private void SetRoomInfo()
    {
        roomName_TEXT.text = string.Format("Room : " + PhotonNetwork.CurrentRoom.Name + " Room");
        playerCount_TEXT.text = string.Format("Player : " + PhotonNetwork.CurrentRoom.PlayerCount +
                                                " / " + PhotonNetwork.CurrentRoom.MaxPlayers);
    }
    /// <summary>
    /// 방에 입장한 인원 수를 업데이트 처리
    /// </summary>
    private void UpdatePlayerCount()
    {
        playerCount_TEXT.text = string.Format("Player : " + PhotonNetwork.CurrentRoom.PlayerCount +
                                                " / " + PhotonNetwork.CurrentRoom.MaxPlayers);
    }

    /// <summary>
    /// 생성지점에 player가 들어오면 캐릭터프리팹 생성
    /// </summary>
    // private void CreateCharacter()
    // {
    //     if (characters == null)
    //     {
    //         Debug.Log("생성된 캐릭터가 없습니다.");    
    //     }
    //     else
    //     {
    //         GameObject go = PhotonNetwork.Instantiate("Prefabs/Character/Player", RespawnSpot.transform.position, Quaternion.identity);
    //     }
    // }
    #endregion
}
