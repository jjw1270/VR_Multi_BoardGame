using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class PlayerCtrl : MonoBehaviour
{
    private GameObject cursorImage;
    //private GameObject ReadyGuide;
    private Vector3 screenCenter;
    private bool isTriggerd;
    private string myTag;

    void Start()
    {
        //ReadyGuide = GameObject.Find("ReadyGuide");
        cursorImage = GameObject.Find("BTNEnable");

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
            if(hit.transform.CompareTag(myTag)){  //자신의 것만 활성화
                cursorImage.SetActive(true);
                if(isTriggerd){
                    btnCtrl(hit.transform);
                }
            }
            else{
                cursorImage.SetActive(false);
            }
        }        
    }

    void btnCtrl(Transform hit){
        if(hit.parent.name == "BTN"){
            //ReadyGuide.SetActive(false);
            hit.GetComponent<BTNCtrl>().isClick = true;
        }
    }
}
