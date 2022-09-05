﻿using UnityEngine;
using UnityEngine.UI;
using Photon.Voice.PUN;
using Photon.Pun;
using Photon.Realtime;

public class VoiceUI : MonoBehaviour
{
    [SerializeField] Image speaking_IMG;
    [SerializeField] Image recording_IMG;
    [SerializeField] Text playerName_TEXT;

    #region Private variable
    //private Canvas canvas;
     [SerializeField] PhotonView pv;
    [SerializeField] PhotonVoiceView pv_voice;
    public GameObject characters;

    #endregion

   

    private void Start()
    {

        TurnOnVoice();

        // 해당 Player가 방에 입장하거나 생성할 때 입력한 닉네임을 불러와 출력
    }

    
    void TurnOnVoice(){

       playerName_TEXT.text = pv.Owner.NickName;

       //print(playerName_TEXT.text);

       GameObject go = Instantiate(characters, RoomManager.InstanceRoomManager.RespawnSpot);
       Text[] texts = go.GetComponentsInChildren<Text>();
       texts[0].text = playerName_TEXT.text; //name of string

    }

    private void Update()
    {
        //speaking_IMG.enabled = pv_voice.IsSpeaking;
        // 상대가 말하는 것을 출력할 때 이미지가 나오도록 출력
        //recording_IMG.enabled = pv_voice.IsRecording;
        // 말하고 있을 때 해당 이미지가 나오도록 출력
    }
}
