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
    public static bool isAllReady = false;
    private bool isSelectGame = false;
    private GameObject selectCanvas;
    private GameObject canvas1;
    private GameObject canvas2;
    public static int player1 = -99;
    public static int player2 = -99;
    public static bool playGame1;
    public static bool playGame2;
    
    GameObject Game1;
    GameObject Game2;

    void Awake(){
        pv = this.GetComponent<PhotonView>();
    }

    void Start(){
        selectCanvas = GameObject.Find("SelectGameCanvas");
        canvas1 = selectCanvas.transform.GetChild(0).gameObject;
        canvas2 = selectCanvas.transform.GetChild(1).gameObject;
        Game1 = GameObject.Find("TicTacToe");
        //Game1.SetActive(false);
        //Game2 = GameObject.Find("TargetFalling");
        //Game2.SetActive(false);
        PhotonNetwork.IsMessageQueueRunning = true;
        Invoke("playerSpawn",0.5f);
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if(isReady1 && isReady2)
            isAllReady = true;
        else
            isAllReady = false;

        if(isAllReady){
            isSelectGame = true;
            canvas1.SetActive(true);
            canvas2.SetActive(true);
        }

        if(isSelectGame){
            isAllReady = false;
            selectGame();
        }
        else{
            canvas1.SetActive(false);
            canvas2.SetActive(false);
        }

        if(player1 > 0 && player2 > 0){
            isSelectGame = false;
            if(playGame1)
                Game1.SetActive(true);
            else
                Game2.SetActive(true);
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

    void selectGame(){
        if(player1 < 0 || player2 < 0) return;
        if(player1 == player2){
            if(player1 == 1){
                playGame1 = true;
                playGame2 = false;
            }
            else{
                playGame2 = true;
                playGame1 = false;
            }
        }
        else{
            int r = Random.Range(0, 2);
            if(r == 1){
                playGame1 = true;
                playGame2 = false;
            }
            else{
                playGame2 = true;
                playGame1 = false;
            }
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