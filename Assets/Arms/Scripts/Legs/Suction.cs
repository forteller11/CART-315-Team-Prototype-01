using System;
using Helpers;
using Legs;
using UnityEngine;
using UnityEngine.Serialization;

public class Suction : MonoBehaviour
    {
        private CircleCollider2D[] _suctionColliders2D;
        public CircleCollider2D[] SuctionColliders2D
        {
            get => _suctionColliders2D;
        }

        [SerializeField]
        public BoxCollider2D _suckThresholdCollider;

        private void Awake() => _suctionColliders2D = GetComponents<CircleCollider2D>();
    }
