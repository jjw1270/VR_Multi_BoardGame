using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    Transform spawnPoints;
    PhotonView pv;
    public bool isFull1;  //자리가 찼는지
    public bool isFull2;
    public static bool isReady1 = false;  //버튼이 눌렸는지
    public static bool isReady2 = false;
    bool isAllReady = false;

    void Awake(){
        pv = this.GetComponent<PhotonView>();
    }

    void Start(){
        PhotonNetwork.IsMessageQueueRunning = true;
        Invoke("playerSpawn",0.5f);
    }
    void Update(){
        //Debug.Log(isReady1);
        if(Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if(isReady1 && isReady2)
            isAllReady = true;
        else
            isAllReady = false;

        if(isAllReady){
            Debug.Log("12312321");
        }
    }

    void playerSpawn(){
        if(!isFull1 && !isFull2){
            //1번 자리 스폰
            GameObject spawnPoint = GameObject.FindWithTag("1");
            GameObject player = PhotonNetwork.Instantiate("Player", spawnPoint.transform.position, spawnPoint.transform.rotation);
            player.tag = "1";
            pv.RPC("isFull", RpcTarget.AllBufferedViaServer, 1);
        }
        else if(isFull1 && !isFull2){
            //2번 자리 스폰
            GameObject spawnPoint = GameObject.FindWithTag("2");
            GameObject player =  PhotonNetwork.Instantiate("Player", spawnPoint.transform.position, spawnPoint.transform.rotation);
            player.tag = "2";
            pv.RPC("isFull", RpcTarget.AllBufferedViaServer, 2);
        }
    }

    [PunRPC]
    void isFull(int f){
        if(f == 1){
            isFull1 = true;
        }
        else{
            isFull2 = true;
        }
    }
}