using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BTNCtrl : MonoBehaviourPunCallbacks
{
    PhotonView pv;
    public bool isClick;
    public bool isGreen;
    GameManager gm;
    void Awake(){
        pv = this.GetComponent<PhotonView>();
    }
    void Start(){
        gm = transform.Find("GameManager").GetComponent<GameManager>();
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
        }
    }
    [PunRPC]
    void BtnColor(int j){
        if(j == 1)
            this.GetComponent<MeshRenderer>().material.color = Color.green;
            
        else
            this.GetComponent<MeshRenderer>().material.color = Color.red;
    }
}
