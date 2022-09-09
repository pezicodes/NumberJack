
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
public class RoomUI : UIManager
{
    [Header("Room UI Container")]
    [SerializeField] GameObject Roominfo_Container;
    [SerializeField] GameObject Icon_Container;

    [Header("RoomInfo UI")]
    [SerializeField] Text roomName_TEXT;
    [SerializeField] Text playerCount_TEXT;

    [Header("Icon UI")]
    [SerializeField] Button exit_BTN;
  //  [SerializeField] Button cam_BTN;
  //  [SerializeField] Text cam_TEXT;
   // [SerializeField] Sprite toggle_LEFT;
   // [SerializeField] Sprite toggle_RIGHT;

    [SerializeField] RoomManager roomManager;

    #region public variable
    // public bool isFPV = false;
    public int click_btn = 0;
    #endregion

    #region LifeCycle
    public override void Awake()
    {
        roomManager = GameObject.FindObjectOfType<RoomManager>();
    }
    private void Start()
    {
        exit_BTN.onClick.AddListener(Exit_BTN);
        //cam_BTN.onClick.AddListener(Change_Cam);
    }
    #endregion

    #region Public Method
    public override void Exit_BTN()
    {   
        
        PhotonNetwork.LeaveRoom(); 
        
        RoomManager.InstanceRoomManager.RoomContent.SetActive(false);
        RoomManager.InstanceRoomManager.PlayerNumber.SetActive(false);  
        RoomManager.InstanceRoomManager.GoHome.SetActive(true);  

        //PhotonNetwork.Disconnect();
    
    }
    #endregion

}
