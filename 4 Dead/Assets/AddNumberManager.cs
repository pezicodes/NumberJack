
using UnityEngine;

using UnityEngine.UI;

using Photon.Pun;
using Photon.Chat;
using ExitGames.Client.Photon;
using Photon.Realtime;

public class AddNumberManager : MonoBehaviourPunCallbacks, IChatClientListener
{
    public Text chatView_TEXT;
    public Text chatView_INPUT;
    public PhotonView pv_Chat;

    public static AddNumberManager InstanceAddNumber;

    #region Private variable
    private string playerName;
    private string cur_chatChannel;
    private ChatClient chatClient;
    private ScrollRect scrollRect = null;
    #endregion

    #region LifeCycle
    private void Start()
    {

        InstanceAddNumber = this;
        scrollRect = GameObject.FindObjectOfType<ScrollRect>();
        Application.runInBackground = true;
        // 앱이 백그라운드 상태일 때 실행되도록 설정처리

        playerName = PlayerPrefs.GetString("User_Name");
        // PlayerPrefs에 저장한 User_Name의 Key의 value값을 가져온다

        cur_chatChannel = "002";
        chatClient = new ChatClient(this);

        chatClient.UseBackgroundWorkerForSending = true;
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, "2.0.0",
                            new Photon.Chat.AuthenticationValues(playerName));
        // player의 이름과 포톤네트워크의 ChatID, 버전을 통해 연결 시도
    }

    private void Update()
    {
        chatClient.Service();

    }
    #endregion

    #region Public Method
    /// <summary>
    /// 채팅목록에 입력한 내용이 1줄씩 올라가도록 처리하는 메서드
    /// </summary>
    public void AddLine(string _chatLine)
    {
        chatView_TEXT.text += _chatLine + "\n";

        Debug.Log("This is the chat message" + _chatLine);
    }

    public void OnApplicationQuit()
    {
        if (chatClient != null)
        {
            chatClient.Disconnect();
        }
    }


    public void SendChat(string _inputTEXT)
    {
        if (string.IsNullOrEmpty(_inputTEXT))
        {
            return;
        }

        if (chatClient.State == ChatState.ConnectedToFrontEnd)
        {
            chatClient.PublishMessage(cur_chatChannel, _inputTEXT);
            chatView_INPUT.text = "";
        }
    }

    public void SendMessage()
    {
        SendChat(chatView_INPUT.text);
        chatView_INPUT.text = "";
    }



    #endregion

    #region Pun Method
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        // pv_Chat.RPC("PlayerState", RpcTarget.All, "<color=yellow>" + newPlayer.NickName + "You have participated.</color>");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //pv_Chat.RPC("PlayerState", RpcTarget.All, "<color=red>" + otherPlayer.NickName + "You have left.</color>");
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient) //isMaasterClient
        {
            //pv_Chat.RPC("PlayerState", RpcTarget.All, 
            //"<color=green>Wardrobe[" + PhotonNetwork.NickName + "]You have participated.</color>");


        }
    }
    #endregion

    #region RPC
    /// <summary>
    /// RPC를 통해 방에 존재하는 player의 채팅창에 player의 상태를 전달
    /// </summary>
    /// <param name="message"></param>
    [PunRPC]
    private void PlayerState(string message)
    {
        // chatView_TEXT.text = message;   
    }
    #endregion

    #region IChatClientListener
    public override void OnConnected()
    {
        chatClient.Subscribe(new string[] { cur_chatChannel }, 10);
    }

    public void OnDisconnected()
    {
        chatView_TEXT.text = "";
    }

    public void OnChatStateChange(ChatState state)
    {
        Debug.Log("OnChatState : " + state);
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        //  AddLine(string.Format("Channel ({0}) connection", string.Join(",", channels)));
    }

    public void OnUnsubscribed(string[] channels)
    {
        //  AddLine(string.Format("Channel {0} connection termination", string.Join(",", channels)));
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        if (level == ExitGames.Client.Photon.DebugLevel.ERROR)
        {
            Debug.LogError(message);
        }
        else if (level == ExitGames.Client.Photon.DebugLevel.WARNING)
        {
            Debug.LogWarning(message);
        }
        else
        {
            Debug.Log(message);
        }
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        if (channelName.Equals(cur_chatChannel))
        {
            for (int i = 0; i < messages.Length; i++)
            {

                AddLine(string.Format("[{0}]:{1}", senders[i], messages[i].ToString()));

                Debug.LogWarning("It's Working");

                if (!(senders[i].Contains(PhotonNetwork.LocalPlayer.NickName)))
                {

                    #region RELAYING PLAYER'S NUMBERS TO NUMBE JACK BACK END

                    // entry =  5678

                    PlayerPrefs.SetString("PLAYER_NUM", messages[i].ToString());                   
                    MultiplayerManager.multiPlay.generateNumber();

                    // result = 5678


                    #endregion


                }
            }




        }

    }
    #endregion
    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        throw new System.NotImplementedException();
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserSubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }


}
