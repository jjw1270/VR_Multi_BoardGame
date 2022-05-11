using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class PlayerCtrl : MonoBehaviour
{
    private GameObject cursorImage;
    private Vector3 screenCenter;
    private bool isTriggerd;
    private string myTag;
    void Start()
    {
        cursorImage = GameObject.Find("CursorEnable");
        
        screenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
        myTag = this.transform.tag;
        
        cursorImage.SetActive(false);
    }
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit hit;
        isTriggerd = Input.GetMouseButtonDown(0);

        if(Physics.Raycast(ray, out hit, 500f)){
            if(hit.transform.CompareTag(myTag) || hit.transform.CompareTag("1and2")){  //자신의 것만 활성화
                cursorImage.SetActive(true);
                if(isTriggerd){
                    ClickEvent(hit.transform);
                }
            }
            else{
                cursorImage.SetActive(false);
            }
        }

    }

    void ClickEvent(Transform hit){
        if(hit.parent.name == "BTN"){
            hit.GetComponent<BTNCtrl>().isClick = true;
        }
        if(hit.parent.CompareTag("SelectGame")){
            hit.GetComponent<Image>().color = Color.blue;
            if(hit.name == "Game1"){
                if(myTag == "1")
                    GameManager.player1 = 1;
                else
                    GameManager.player2 = 1;
            }
            else{
                if(myTag == "1")
                    GameManager.player1 = 2;
                else
                    GameManager.player2 = 2;
            }
        }
        if(hit.parent.parent.name == "Points"){
            Debug.Log("FFF");
        }

    }
}