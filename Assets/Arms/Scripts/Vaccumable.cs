using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

public class Vaccumable : MonoBehaviour, IPoolable
{
    public GameObjectPool Pool { get; set; }

    private void OnEnable()
    {
        //Randomize object type
    }
}
