using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class TicTacToe : MonoBehaviourPunCallbacks
{
    public static bool[,] isFull = new bool[3, 3];
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.playGame1){
            




            
        }
    }
}
