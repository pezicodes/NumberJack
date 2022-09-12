using UnityEngine;
using UnityEngine.UI;
using Photon.Voice.PUN;
using Photon.Pun;
using Photon.Realtime;

public class VoiceUI : MonoBehaviour
{
    [SerializeField] Image speaking_IMG;
    [SerializeField] Image recording_IMG;
    public Text playerName_TEXT;

    #region Private variable
    //private Canvas canvas;
     [SerializeField] PhotonView pv;
    [SerializeField] PhotonVoiceView pv_voice;

    public static VoiceUI playerkey;
    
    public GameObject MyPlayer;

    private string PlayerName;

    #endregion

   

    private void Start()
    {
        playerkey = this;
        TurnOnVoice();

    }

    
    void TurnOnVoice(){

       playerName_TEXT.text = pv.Owner.NickName;
       PlayerName = MyPlayer.GetComponentInChildren<Text>().text;
       GameObject PlayerAvatar = Instantiate(RoomManager.InstanceRoomManager.PlayerAvatarName, RoomManager.InstanceRoomManager.RespawnSpot);
       Text[] texts = PlayerAvatar.GetComponentsInChildren<Text>();
       texts[0].text = playerName_TEXT.text;

       if(playerName_TEXT.text == PlayerPrefs.GetString("Username")){
            return;
       }

       PlayerPrefs.SetString("OpponentName", playerName_TEXT.text);

    }

    private void Update()
    {
        //speaking_IMG.enabled = pv_voice.IsSpeaking;
       
        //recording_IMG.enabled = pv_voice.IsRecording;
     
        
    }

    public void Kick()
    {
        PhotonNetwork.EnableCloseConnection(true);

        Kick(PlayerPrefs.GetString("OpponentName"));
        Debug.Log("Kicked Out of Room");
        Debug.Log(PlayerPrefs.GetString("OpponentName"));

    }
    private void Kick(Text PlayerName)
    {
        if (PlayerName == null)
        {
            return; // log error?
        }
        string nickname = PlayerName.text;
        Kick(nickname);
    }

    private void Kick(string nickname)
    {
        if (string.IsNullOrEmpty(nickname))
        {
            return; // log error?
        }

        for (int i = 0; i < PhotonNetwork.PlayerListOthers.Length; i++)
        {
            Player player = PhotonNetwork.PlayerListOthers[0];
            if (!player.IsLocal && player.NickName.Equals(PlayerPrefs.GetString("OpponentName")))
            {
                Kick(player);
                return;
            }
        }
      
    }

    private void Kick(Player playerToKick)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return; // log error?
        }
        PhotonNetwork.CloseConnection(playerToKick);
    }
}

    

