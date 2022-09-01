using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class ServerManager : MonoBehaviourPunCallbacks
{
    #region singleton
    private static ServerManager instance;
    public static ServerManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new ServerManager();
            }
            return instance;
        }
    }
    #endregion

    #region lifecycle
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    #region public Method
    /// <summary>
    /// 서버 연결이 안되거나 안되어있을 때 서버재연결을 수행하는 메서드
    /// </summary>
    public void ConnectedToServer()
    {
        PhotonNetwork.GameVersion = PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion;
        PhotonNetwork.ConnectUsingSettings();
    }
    #endregion

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log(cause);
        ConnectedToServer();
    }
}
