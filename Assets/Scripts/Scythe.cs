using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Wheat wheat = other.gameObject.GetComponent<Wheat>();
        if (wheat && wheat.isGrown)
        {
            wheat.Cut();
        }
    }
}
