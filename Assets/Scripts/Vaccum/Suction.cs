using System;
using System.Collections.Generic;
using Helpers;
using Legs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

//resposible for checking collisions with nearby walls to determine the amount of suction currently occuring
public class Suction : MonoBehaviour
{
    [SerializeField] LayerMask _layersToSuction;
    
    Rigidbody2D _rigidBody;
    public int SuctionCollidersCount { get => _suctionColliders2D.Length; }
    private CircleCollider2D[] _suctionColliders2D;
    

    private void Awake()
    {
        _suctionColliders2D = GetComponents<CircleCollider2D>();
        _rigidBody = GetComponent<Rigidbody2D>();
        if (_layersToSuction == 0)
            Debug.LogWarning("_layersToSuction as not been set, which may cause problems!");
    }
    
    //returns a list of all the positions where the suction is colliding
    public Vector2[] GetSuctionPoints()
    {
        List<Vector2> collisionSuctionPoints = new List<Vector2>();

        for (int i = 0; i < _suctionColliders2D.Length; i++)
        {
            //transform colliders positions based off rigidbody rotation and collider offsets
            float rotZ = _rigidBody.rotation * Mathf.Deg2Rad;
            float2 offset = _suctionColliders2D[i].offset;
            float2 iBase = new float2(math.cos(rotZ), math.sin(rotZ)) * _rigidBody.transform.localScale.x;
            float2 jBase = new float2(-math.sin(rotZ), math.cos(rotZ)) * _rigidBody.transform.localScale.y;
            float2x2 rotScaleMat = new float2x2(iBase, jBase);
            Vector2 localOffsetTransformed = math.mul(rotScaleMat, offset);
            var circleColliderPos = localOffsetTransformed + _rigidBody.position;
            
            //test collision
            Collider2D suctionCollision = Physics2D.OverlapCircle(circleColliderPos, _suctionColliders2D[i].radius, _layersToSuction);
            Debug.DrawLine(_rigidBody.position, circleColliderPos, new Color(1f, 1, 0, 0.5f));

            //if collision happened, record the position and number
            if (suctionCollision != null)
            {
                collisionSuctionPoints.Add(circleColliderPos);
            }
            
        }

        return collisionSuctionPoints.ToArray();
    }
    
    }
