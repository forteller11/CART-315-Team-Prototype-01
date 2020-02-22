using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

public class Segment : MonoBehaviour, IPoolable
{
    public Segment Next;
    public Segment Previous;
    
    
    public void SetActiveTo(bool trueFalse)
    {
        gameObject.SetActive(trueFalse);
    }

    public Action<IPoolable> ReturnToPool { get; set; }
    public GameObject PooledGameObject { get; set; }
    
}


