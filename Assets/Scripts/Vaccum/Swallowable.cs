using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using Legs;
using UnityEngine;

public class Swallowable : MonoBehaviour, IPoolable
{
    public CombinableType Type;
    [SerializeField] private CombineManager CombineManager;
    public GameObjectPool Pool { get; set; }
    [HideInInspector] public bool BeginCheckingForCombinations;
    [Flags]
    public enum CombinableType
    {
        Energy        = 0b_0000_0001,
        Liquid        = 0b_0000_0010,
        Clothes       = 0b_0000_0100
    }

    [HideInInspector] public Rigidbody2D Rigidbody2D;

    private void Awake() => Rigidbody2D = GetComponent<Rigidbody2D>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (BeginCheckingForCombinations == false)
            return;

        var otherSwallowable = other.GetComponent<Swallowable>();
        if (otherSwallowable == null) //make sure other item is of combinable/swalloable type
            return;

        //manage powerups
        CombineManager.OnCombination(this, otherSwallowable);


    }
}
