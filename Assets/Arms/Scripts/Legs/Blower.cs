using System;
using Helpers;
using Legs;
using UnityEngine;
using UnityEngine.Serialization;

public class Blower : MonoBehaviour
    {
        [FormerlySerializedAs("suctionColliders2D")] [SerializeField] private Collider2D[] _suctionColliders2D;
        public Collider2D[] SuctionColliders2D { get; }
        
    }
