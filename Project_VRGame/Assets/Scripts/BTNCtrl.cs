using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BTNCtrl : MonoBehaviourPunCallbacks
{
    PhotonView pv;
    public bool isClick;
    public bool isGreen;
    string myTag;
    void Awake(){
        pv = this.GetComponent<PhotonView>();
    }
    void Start(){
        myTag = this.tag;
    }
    void Update(){
        if(isClick){
            if(!isGreen){
                isGreen = true;
                pv.RPC("BtnColor", RpcTarget.AllBufferedViaServer, 1);
            }
            else{
                isGreen = false;
                pv.RPC("BtnColor", RpcTarget.AllBufferedViaServer, 0);
            }
            isClick = false;

            if(this.name == "TTT_ResetBTN"){
                //TTT게임 종료, 다시 SelectGame
            }
        }
    }
    [PunRPC]
    void BtnColor(int j){
        if(j == 1){
            this.GetComponent<MeshRenderer>().material.color = Color.green;
            if(myTag == "1")
                GameManager.isReady1 = true;
            else
                GameManager.isReady2 = true;
        }
        else{
            this.GetComponent<MeshRenderer>().material.color = Color.red;
            if(myTag == "1")
                GameManager.isReady1 = false;
            else
                GameManager.isReady2 = false;
        }
    }
}
