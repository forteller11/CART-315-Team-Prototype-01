using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaccumObjects : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        var vaccumable = other.GetComponent<Vaccumable>();
        if (vaccumable == null)
            return;

        other.GetComponent<SpriteRenderer>().color = Color.green;
    }

//    private void OnTriggerEnter(Collider other)
//    {
//        Destroy(other);
//    }
}
