using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwallowObjects : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        var vaccumable = other.GetComponent<Vaccumable>();
        if (vaccumable == null)
            return;

        other.GetComponent<SpriteRenderer>().color = Color.green;
    }
    
}
