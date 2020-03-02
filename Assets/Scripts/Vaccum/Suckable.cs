using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

public class Suckable : MonoBehaviour, IPoolable
{
    public GameObjectPool Pool { get; set; }
    public enum SuckableType
    {
        NotCombinable,
        Energy,
        Liquid,
        Clothes
    }

    public SuckableType Type = SuckableType.NotCombinable;

    private void OnEnable()
    {
        //Randomize object type
    }
}
