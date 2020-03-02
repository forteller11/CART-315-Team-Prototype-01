using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

public class Swallowable : MonoBehaviour, IPoolable
{
    public GameObjectPool Pool { get; set; }
    public enum SuckableType
    {
        NonCombinable,
        Energy,
        Liquid,
        Clothes
    }

    public Rigidbody2D Rigidbody2D;

    private void Awake() => Rigidbody2D = GetComponent<Rigidbody2D>();

    public SuckableType Type = SuckableType.NonCombinable;
}
