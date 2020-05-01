﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballRespawn : MonoBehaviour
{

    public GameObject originalPos;
    public GameObject spawnObject;
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            other.tag = "Untagged";
            Instantiate(spawnObject, originalPos.transform.position, Quaternion.identity);
        }
    }
}
