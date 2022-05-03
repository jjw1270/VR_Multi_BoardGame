using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
# if PLATFORM_ANDROID
using UnityEngine.Android;
# endif
public class LobyManager : MonoBehaviourPunCallbacks
{
    public string version = "v1.0";
    //public InputField userId; //플레이어 이름을 입력하는 UI 항목 연결
    public string userId = "userID";
    public static byte maxPlayer = 2;
    GameObject blackEffect;
    bool isEnterRoom = false;
    void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
        blackEffect = GameObject.Find("BlackEffect");
    }

    void Start() {
         #if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
        #endif
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        if(isEnterRoom)
            blackEffect.transform.position = Vector3.Lerp(blackEffect.transform.position, new Vector3(0,2.45f,0.9f), 0.1f);
    }

    public override void OnConnectedToMaster() //포톤 클라우드에 접속이 잘되면 호출되는 콜백함수
    {
        base.OnConnectedToMaster();
        Debug.Log("Entered Lobby");
        userId = GetUserId();
    }
    string GetUserId() //로컬에 저장된 플레이어 이름을 반환하거나 생성하는 함수
    {
        // string userId = PlayerPrefs.GetString("USER_ID");
        // if (string.IsNullOrEmpty(userId))
        // {
        //     userId = "USER_" + Random.Range(0, 999).ToString("000");
        // }
        userId = "USER_" + Random.Range(0, 999).ToString("000");
        return userId;
    }
    public override void OnJoinRandomFailed(short returnCode, string message) //방 입장이 실패했을 때
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log("No Room!!");
        PhotonNetwork.CreateRoom("MyRoom", new RoomOptions { MaxPlayers = maxPlayer }); //방을 만들어줌
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("방 입장");
        SceneManager.LoadScene("MainGameScene");
        PhotonNetwork.IsMessageQueueRunning = false;
    }
    public void EnterRoom(GameObject door){
        //GameManager.Instance.enableBtn();
        //Debug.Log("방입장");
        Animator anm = door.transform.root.GetComponent<Animator>();
        anm.SetBool("open", true);
        Invoke("BlackEffect", 4.0f);
    }
    public void BlackEffect(){
        isEnterRoom = true;
        Invoke("MainScene", 0.5f);
    }
    void MainScene(){
        PhotonNetwork.JoinRandomRoom();
    }
}