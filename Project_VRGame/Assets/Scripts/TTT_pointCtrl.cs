using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TTT_pointCtrl : MonoBehaviourPunCallbacks
{
    public bool isFull = false;
    PhotonView pv;
    void Start()
    {
        pv = this.GetComponent<PhotonView>();
    }
    void Update()
    {
        if(isFull){
            pv.RPC("syncP", RpcTarget.AllViaServer);
        }
    }


    [PunRPC]
    void syncP(){
        isFull = true;
    }
}
