using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using Legs;
using UnityEngine;

public class Swallowable : MonoBehaviour, IPoolable
{
    public SuckableType Type = SuckableType.NonCombinable;
    public GameObjectPool Pool { get; set; }
    [HideInInspector] public bool BeginCheckingForCombinations;
    [Flags]
    public enum SuckableType
    {
        NonCombinable = 0b_0000_0001,
        Energy        = 0b_0000_0010,
        Liquid        = 0b_0000_0100,
        Clothes       = 0b_0000_1000
    }

    [HideInInspector] public Rigidbody2D Rigidbody2D;

    private void Awake() => Rigidbody2D = GetComponent<Rigidbody2D>();
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (BeginCheckingForCombinations == false)
            return;

        var otherSwallowable = other.GetComponent<Swallowable>();
        if (otherSwallowable == null)
            return;

        var combinedType = otherSwallowable.Type | Type;

        //call methods in body approrpaite power ups
        switch (combinedType)
        {
            case SuckableType.Liquid | SuckableType.Energy:
                VaccumBodySingleton.Instance.GetComponent<SpriteRenderer>().color = Color.red;
                this.Pool.ReturnToPool(this.gameObject);
                otherSwallowable.Pool.ReturnToPool(otherSwallowable.gameObject);
                
                //start coroutine to force multiplier in settings....
                
                //at end it changes it back
                break;
        }
        
    
    }
}
