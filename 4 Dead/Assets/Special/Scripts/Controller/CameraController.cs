using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class CameraController : MonoBehaviourPun
{
    #region Singleton
    private static CameraController instance;
    public static CameraController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new CameraController();
            }
            return instance;
        }
    }
    #endregion

    #region Public variable
    public Transform cam_target;
    #endregion

    #region Private variable
    private RoomUI roomUI;
    private PhotonView pv;

    [SerializeField] Camera firstCam;
    [SerializeField] Camera thirdCam;
    #endregion

    #region LifeCycle
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        firstCam.enabled = false;
        thirdCam.enabled = false;
        roomUI = GameObject.FindObjectOfType<RoomUI>();
        pv = this.gameObject.GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (!pv.IsMine)
        // 자신의 캐릭터가 아닐 경우 작동하지 않도록 처리
        {
            return;
        }
        else if (pv.IsMine)
        // 자신의 캐릭터일 경우 초기 시점이 3인칭(TPV)로 초기화
        {
            SetCamera_Third();
        }
    }

    private void Update()
    {
        this.gameObject.transform.position = cam_target.position;
        // 캐릭터의 움직임에 따라 카메라의 위치가 따라가도록 처리
    }
    #endregion

    #region Public Method
    /// <summary>
    /// FPV 카메라 연결
    /// </summary>
    public void SetCamera_First()
    {
        firstCam.enabled = true;
        thirdCam.enabled = false;
    }
    /// <summary>
    /// TPV 카메라 연결
    /// </summary>
    public void SetCamera_Third()
    {
        thirdCam.enabled = true;
        firstCam.enabled = false;
    }
    #endregion
}
