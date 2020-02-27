using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suck : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("suck");
        Destroy(other);
    }

//    private void OnTriggerEnter(Collider other)
//    {
//        Destroy(other);
//    }
}
