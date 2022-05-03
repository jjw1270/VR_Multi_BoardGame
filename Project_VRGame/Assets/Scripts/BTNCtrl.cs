using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BTNCtrl : MonoBehaviour
{
    PhotonView pv;
    public bool isClick;
    void Awake(){
        pv = this.GetComponent<PhotonView>();
    }
    void Update(){
        if(!pv.IsMine) return;
        if(isClick){
            pv.RPC("BtnColor", RpcTarget.AllBufferedViaServer);
            isClick = false;
        }
    }

    [PunRPC]
    void BtnColor(){
        this.GetComponent<MeshRenderer>().material.color = Color.green;
    }
}
