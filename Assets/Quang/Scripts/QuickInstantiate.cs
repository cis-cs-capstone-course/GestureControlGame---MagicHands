﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
public class QuickInstantiate : MonoBehaviour
{
  [SerializeField]
  private GameObject _prefab;
  
  private void Awake()
  {   
        //Vector2 offset = Random.insideUnitCircle * 3f;
        //Vector3 position = new Vector3(transform.position.x + offset.x, transform.position.y + offset.y, transform.position.z);
        Vector3 position = transform.position;
        MasterManager.NetworkInstantiate(_prefab, position, Quaternion.identity);
  }
 
}
