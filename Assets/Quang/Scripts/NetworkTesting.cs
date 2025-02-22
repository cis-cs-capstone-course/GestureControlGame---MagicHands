﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkTesting : MonoBehaviourPunCallbacks
{
    private void Start()
    {   
        
        print("Connecting to Server.");
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = MainMenu.photonNickname;
        PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("Connected to server");
        print(PhotonNetwork.LocalPlayer.NickName);
        if (!PhotonNetwork.InLobby)
        {
            Debug.Log("time spent for connecting to server: " + (Time.realtimeSinceStartup - TimeCounter.timeCounter.lastTime) + " seconds");
            PhotonNetwork.JoinLobby();
        }
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Disconnected from server for reason " + cause.ToString());
    }

}
