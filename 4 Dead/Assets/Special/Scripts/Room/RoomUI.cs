
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
    public bool isFPV = false;
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
    /// <summary>
    /// FPV / TPV 시점을 전환처리하는 메서드
    /// </summary>
    public void Change_Cam()
    { 
        if (isFPV == false)
        {
            CameraController.Instance.SetCamera_Third();
            //cam_TEXT.text = string.Format("TPV");
            //cam_BTN.image.sprite = toggle_LEFT;
            isFPV = true;
     
        }
        else if (isFPV == true)
        {
            CameraController.Instance.SetCamera_First();
           // cam_TEXT.text = string.Format("FPV");
           // cam_BTN.image.sprite = toggle_RIGHT;
            isFPV = false;

        }
    }

    public override void Exit_BTN()
    {
        PhotonNetwork.LeaveRoom();
        Debug.Log(PhotonNetwork.CurrentRoom.Name + "Room => Lobby 이동");
        SceneManager.LoadScene("Lobby");
    }
    #endregion

}
