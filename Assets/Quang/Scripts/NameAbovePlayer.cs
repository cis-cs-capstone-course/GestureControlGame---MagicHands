﻿using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
 
 public class NameAbovePlayer : MonoBehaviourPunCallbacks
 {
     public GUISkin guiSkin; // choose a guiStyle (Important!)
    
     private PhotonView PV;
     public string text;
 
     public Color color = Color.white;   // choose font color/size
     public float fontSize = 40;
     public float offsetX = 0.60f;
     public float offsetY = -25f;
 
     float boxW = 150;
     float boxH = 20;
 
     public bool messagePermanent = true;
 
     public float messageDuration { get; set; }
 
     Vector2 boxPosition;
     void Start()
     {           
         PV = GetComponent<PhotonView>();
         if(PV.IsMine)
            text = PhotonNetwork.NickName;
         else
            text = PV.Owner.NickName;

         if(messagePermanent)
         {
             messageDuration = 1;
         }
     }
     void OnGUI()
     {
         if(messageDuration > 0)
         {
             if(!messagePermanent) // if you set this to false, you can simply use this script as a popup messenger, just set messageDuration to a value above 0
             {
                 messageDuration -= Time.deltaTime;
             }
 
             GUI.skin = guiSkin;
             boxPosition = Camera.main.WorldToScreenPoint(transform.position);
             boxPosition.y = Screen.height - boxPosition.y;
             boxPosition.x -= boxW * 0.1f;
             boxPosition.y -= boxH * 0.5f;
             guiSkin.box.fontSize = (int) fontSize;
 
             GUI.contentColor = color;
 
             Vector2 content = guiSkin.box.CalcSize(new GUIContent(text));
 
             GUI.Box(new Rect(boxPosition.x - content.x / 2 * offsetX, boxPosition.y + offsetY, content.x, content.y), text);
         }
     }

}