using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using Photon.Realtime;

public class PlayerCam : MonoBehaviourPunCallbacks
{
    void Start()
    {
        if(!photonView.IsMine){
            GetComponent<Camera>().enabled = false;
            GetComponent<AudioListener>().enabled = false;
            return;
        }
    }
}