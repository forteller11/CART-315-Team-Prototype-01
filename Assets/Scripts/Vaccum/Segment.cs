using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

public class Segment : MonoBehaviour, IPoolable
{
    public Segment Next;
    public Segment Previous;

    public GameObjectPool Pool { get; set; }
}


