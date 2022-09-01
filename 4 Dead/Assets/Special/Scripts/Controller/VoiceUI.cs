using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Voice.PUN;
using Photon.Voice.Unity;
using Photon.Pun;
using Photon.Realtime;

public class VoiceUI : MonoBehaviour
{
    [SerializeField] Image speaking_IMG;
    [SerializeField] Image recording_IMG;
    [SerializeField] Text playerName_TEXT;

    #region Private variable
    private Canvas canvas;
    private PhotonView pv;
    private PhotonVoiceView pv_voice;
    private Player player;
    #endregion

    #region LifeCycle
    private void Awake()
    {
        pv = this.gameObject.GetComponentInParent<PhotonView>();
        pv_voice = this.gameObject.GetComponent<PhotonVoiceView>();
        canvas = this.GetComponent<Canvas>();
    }

    private void Start()
    {
        playerName_TEXT.text = pv.Owner.NickName;
        // 해당 Player가 방에 입장하거나 생성할 때 입력한 닉네임을 불러와 출력
    }

    private void Update()
    {
        speaking_IMG.enabled = pv_voice.IsSpeaking;
        // 상대가 말하는 것을 출력할 때 이미지가 나오도록 출력
        recording_IMG.enabled = pv_voice.IsRecording;
        // 말하고 있을 때 해당 이미지가 나오도록 출력
    }
    #endregion
}
