
using UnityEngine;

using Photon.Pun;


public class RoomMasterCheck : MonoBehaviour
{
    [PunRPC]
    private void KickPlayer()
    {
        PhotonNetwork.LeaveRoom(); // load lobby scene, returns to master server
    }
    
    public void SendKickPlayer(int playerID)
    {
        PhotonPlayer player = PhotonPlayer.Find(playerID);
    
        PhotonView.RPC("KickPlayer",player);
    }
}
