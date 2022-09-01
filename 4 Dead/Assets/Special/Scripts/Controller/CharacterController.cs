using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

// 설정에 대한 실수에 대비 처리
[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class CharacterController : MonoBehaviourPunCallbacks, IPunObservable
{
    #region Private variable
    private Transform player_Tr;
    private Vector3 cur_Pos;
    private Quaternion cur_Rot;
    private JoyStickController joyStickController;
    private PhotonView pv;

    private bool IsRun;
    private bool IsIdle;
    #endregion

    [SerializeField] Animator player_Anim;

    #region LifeCycle
    private void Awake()
    {
        pv = this.gameObject.GetComponent<PhotonView>();
        player_Tr = this.gameObject.GetComponent<Transform>();
        joyStickController = FindObjectOfType<JoyStickController>();
        player_Tr = this.transform;
    }

    private void Update()
    {
        if(!pv.IsMine)
        // 자신의 캐릭터가 아닐 경우 작동되지 않도록 처리
        {
            return;
        }
        else if (pv.IsMine)
        // 자신의 캐릭터일 경우 조이스틱과 카메라 작동이 되도록 처리
        {
            CameraController.Instance.cam_target = player_Tr;
            this.gameObject.transform.position += joyStickController.move_Vec;
            if (joyStickController.move_Vec == Vector3.zero)
            // 움직임이 없을 경우 Idle애니메이션 출력
            {
                IsIdle = true;
                IsRun = false;
                player_Anim.SetBool("IsIdle", true);
                player_Anim.SetBool("IsRun", false);
            }
            else
            // 움직임이 있을 경우 Run애니메이션 실행
            {
                IsIdle = false;
                IsRun = true;
                player_Anim.SetBool("IsIdle", false);
                player_Anim.SetBool("IsRun", true);
            }
            if (joyStickController.value != null)
            {
                this.gameObject.transform.rotation = Quaternion.Euler(0.0f, Mathf.Atan2(joyStickController.value.x, joyStickController.value.y) * Mathf.Rad2Deg, 0.0f);
            }
        }

        if(!pv.IsMine)
        {
            player_Tr.position = Vector3.Lerp(player_Tr.position, cur_Pos, Time.deltaTime * 5.0f);
            player_Tr.rotation = Quaternion.Slerp(player_Tr.rotation, cur_Rot, Time.deltaTime * 5.0f);
        }
    }
    #endregion

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    // 캐릭터의 움직임을 실시간 처리
    {
        if (stream.IsWriting)
        // 나의 캐릭터의 position과 rotation을 상대에게 보내준다
        {
            stream.SendNext(player_Tr.position);
            stream.SendNext(player_Tr.rotation);
        }
        else
        // 상대가 보낸 데이터를 받아온다
        {
            cur_Pos = (Vector3)stream.ReceiveNext();
            cur_Rot = (Quaternion)stream.ReceiveNext();
        }
    }
}
