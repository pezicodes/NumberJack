using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;


public class IntroManager : ServerManager
{
    #region lifecycle
    private void Awake()
    {
        StartCoroutine(JoinLobby());
    }
    #endregion

    IEnumerator JoinLobby()
    {
        yield return new WaitForSeconds(1.0f);
        AppManager.Instance.ChangeScene(AppManager.eSceneState.Lobby);
        Debug.Log("로비로 이동합니다");
    }
}
