using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Text;

 using UnityEngine.Events;

public class NetworkManagerScript : MonoBehaviourPunCallbacks
{

    public static NetworkManagerScript nm;

    public static Monster[] boardState;

    private PhotonView PV;

    public GameManager manager;

    private void Awake(){
        nm = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PV = GetComponent<PhotonView>();
    }

    public override void OnConnectedToMaster(){
        Debug.Log("Connected");
    }

    public void startGame(){
        PhotonNetwork.CreateRoom("room");
    }

    public void joinGameWithRoomCode(string code){
        PhotonNetwork.JoinRoom(code);
    }

    public void sendBoardData(){
        PV.RPC("receiveBoardData", RpcTarget.All, manager.GetBoardJson());
    }

    [PunRPC]
    public void receiveBoardData(string boardJson)
    {
        Debug.Log("board data received");
        boardState = manager.GetBoardFromJson(boardJson);
        OnBoardUpdate.Invoke();
    }

    public static Monster[] getBoardState(){
        return boardState;
    }

    public delegate void boardUpdate();
    public static event boardUpdate OnBoardUpdate;

}
