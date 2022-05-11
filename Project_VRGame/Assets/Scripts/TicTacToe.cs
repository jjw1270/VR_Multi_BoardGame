using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class TicTacToe : MonoBehaviourPunCallbacks
{
    PhotonView pv;
    int maxGamecount = 5;
    int gameCount = 0;
    public int whosTurn;
    public static int[,] isFull = new int[3, 3];
    public GameObject[] points;
    
    void Start()
    {
        pv = this.GetComponent<PhotonView>();
        resetGame();
    }

    void resetGame(){
        for(int i = 0; i<3; i++){
            for(int j = 0; j<3; j++){
                isFull[i,j] = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.activeSelf){
            while(gameCount < maxGamecount){
                whosTurn = Random.Range(1,3);
                if(whosTurn == 1){
                    pv.RPC("turnCtrl", RpcTarget.AllViaServer, 1);
                }
                else{
                    pv.RPC("turnCtrl", RpcTarget.AllViaServer, 2);
                }
                while(true){
                    
                }

            }




            
        }
    }
    [PunRPC]
    void turnCtrl(int t){
        whosTurn = t;
    }
}
