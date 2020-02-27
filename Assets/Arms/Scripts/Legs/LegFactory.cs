using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using Legs;
using Unity.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.Collections;
using UnityEngine.Assertions;
using Unity.Mathematics;

//responsible for 
public class LegFactory : MonoBehaviour
{
    [Range(0, 100)] public int LegLength = 20;
    [SerializeField] private SLegSettings _settings;
    [SerializeField] private GameObjectPool _legPool;
    [SerializeField] private GameObjectPool _suckablePool;
    [SerializeField] private Material _lineRendererMaterial;
    private Rigidbody2D _rigidbody2D;
    private LineRenderer _lineRenderer;

    private List<Rigidbody2D> _rbs;
    private const float ALIGN_VERTICALLY = -90f;
    private Suction _suction;

    void Start()
    {
        _lineRenderer = new GameObject("Leg Factory Line Renderer").AddComponent<LineRenderer>();
        _lineRenderer.startColor = Color.white;
        _lineRenderer.endColor = Color.white;
        _lineRenderer.positionCount = LegLength;
        _lineRenderer.widthCurve = _settings.ScaleCurve;
        _lineRenderer.widthMultiplier = _settings.WidthMultiplier;
        _lineRenderer.material = _lineRendererMaterial;
        _lineRenderer.material.color = _settings.VaccumSegmentLineColor;
        _lineRenderer.transform.position = new Vector3(0,0f,-20f);
        
        //spawn initial leg attached to leg factory
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _rbs = new List<Rigidbody2D>(LegLength);
        
        var first = _legPool.Spawn();
        first.GetComponent<SpriteRenderer>().color = _settings.VaccumSegmentTint;
        
        SpringJoint2D prevJnt;
        Rigidbody2D prevRb;

        CreateNewSegment(0f, first, _rigidbody2D, out prevJnt, out prevRb);

        _rbs.Add(prevRb);

        for (int i = 1; i < LegLength; i++) //spawn rest of legs
        {
            Segment currentSeg;
            SpringJoint2D currentJnt;
            Rigidbody2D currentRb;

            float normIndex = (float) i / (LegLength-1);
            GameObject current;
            if (i != LegLength - 1)
            {
                current = _legPool.Spawn();
                current.GetComponent<SpriteRenderer>().color = _settings.VaccumSegmentTint;
            }
            else
            {
                current = Instantiate(_settings.VaccumTipPrefab);
                current.name = $" {gameObject.name}'s Blower";
                current.GetComponent<SpriteRenderer>().color = _settings.VaccumTipTint;
                _suction = current.GetComponent<Suction>();
                if (_suction == null) Debug.LogError("_blower not assigned, the prefab is probably not configured properly!");
            }

            CreateNewSegment(normIndex, current, prevRb,  out currentJnt, out currentRb);
            
            _rbs.Add(currentRb);

            prevJnt = currentJnt;
            prevRb = currentRb;

        }
    }

    void CreateNewSegment(float normalizedIndex, in GameObject current, in Rigidbody2D toAttachRb, out SpringJoint2D jnt,
        out Rigidbody2D rb)
    {
        jnt = current.GetComponent<SpringJoint2D>();
        rb = current.GetComponent<Rigidbody2D>();

        float deltaY = (_settings.ScaleCurve.Evaluate(normalizedIndex) * _settings.JointDistance)/2;
        rb.position = toAttachRb.position + new Vector2(0, deltaY);
        jnt.connectedBody = toAttachRb;

        jnt.autoConfigureDistance = false;
        jnt.distance = _settings.JointDistance/2 * _settings.ScaleCurve.Evaluate(normalizedIndex);
        jnt.frequency = _settings.MaxFrequency * _settings.FrequencyCurve.Evaluate(normalizedIndex);
        jnt.dampingRatio = _settings.MaxDampening * _settings.DampeningCurve.Evaluate(normalizedIndex);

        rb.gravityScale = _settings.MaxGravity * _settings.GravityCurve.Evaluate(normalizedIndex);
        rb.mass = _settings.MaxMass * _settings.MassCurve.Evaluate(normalizedIndex);

        current.transform.localScale = _settings.ScaleCurve.Evaluate(normalizedIndex) * _settings.MaxScale;

    }

    void Update()
    {
        for (int i = 0; i < _rbs.Count; i++)
            _lineRenderer.SetPosition(i, _rbs[i].transform.position);
        
        UpdateVaccumSpriteDirections();

    }


    public void MoveLegs(Vector2 inputMove, float inputSuck)
    {

        var blowerRB = _rbs.LastElement();
        int suctionableLayerMask = 0b_0010_0000_0000;
        int suctionCollisions = 0;
        List<Vector2> collisionSuctionPoints = new List<Vector2>();
        
        for (int i = 0; i < _suction.SuctionColliders2D.Length; i++)
        {
            //rotate
            float rotZ = blowerRB.rotation * Mathf.Deg2Rad;
            float2 offset = _suction.SuctionColliders2D[i].offset;
            float2 iBase = new float2( math.cos(rotZ), math.sin(rotZ)) * blowerRB.transform.localScale.x;
            float2 jBase = new float2(-math.sin(rotZ), math.cos(rotZ)) * blowerRB.transform.localScale.y;
            float2x2 rotScaleMat = new float2x2(iBase, jBase);
            Vector2 localOffsetTransformed = math.mul(rotScaleMat, offset);
            //transform offset by rot/scale/transform matrix....
            var circleColliderPos = localOffsetTransformed + blowerRB.position;
            Collider2D suctionCollision = Physics2D.OverlapCircle(circleColliderPos, _suction.SuctionColliders2D[i].radius, suctionableLayerMask);
            Debug.DrawLine(blowerRB.position, circleColliderPos, new Color(1f,1,0,0.5f));

            if (suctionCollision != null)
            {
                suctionCollisions++;
                collisionSuctionPoints.Add(circleColliderPos);
            }
        }
        
        //where 1 == full suction, 0 == no suction
        //amount walll is being sucked vs arm being moved, determined by input and whether close to suctionable (layer-mask) rigidbody
        float suctionAmountNorm = ((float) suctionCollisions / _suction.SuctionColliders2D.Length) * inputSuck;

        blowerRB.angularDrag = Mathf.Lerp(_settings.DampeningOnNoSuction, _settings.DampeningOnSuction, suctionAmountNorm);
        
        float maxForceForEachSuctionPoint = (float) _settings.MaxSuctionForce / _suction.SuctionColliders2D.Length;
        for (int i = 0; i < collisionSuctionPoints.Count; i++)
        {
            var towardsSuction = collisionSuctionPoints[i] - blowerRB.position;
            float forceMult = suctionAmountNorm * maxForceForEachSuctionPoint;
            var forceToApply =  towardsSuction * forceMult;
            blowerRB.AddForce(forceToApply);
            Debug.DrawLine(blowerRB.position, collisionSuctionPoints[i], new Color(.5f,.5f,1,inputSuck));
        }
        //force to body (if suction)
        _rigidbody2D.AddForce(_settings.ForceOnBody * suctionAmountNorm * inputMove);
        
        //force on arms (if not suction)
        for (int i = 0; i < _rbs.Count; i++)
        {
            float indexNorm = (float) i / (_rbs.Count - 1);
            float forceMult = (1 - suctionAmountNorm) * _settings.OpenMaxForce * _settings.OpenForceCurve.Evaluate(indexNorm);
            var forceToAdd = forceMult * inputMove;
            _rbs[i].AddForce(forceToAdd);
            _rigidbody2D.AddForce(-forceToAdd); //so can't float/ drag yourself forward like superman
        }
        

    }
    

    public void SuckVaccumables(float input)
    {
        //check all collisions within cone
        //debug actives
        var sucker = _rbs.LastElement();
        int layerMask = 0b_0001_0000_0000; //8
        var dir =  _rbs[_rbs.Count - 1].position - _rbs[_rbs.Count - 2].position;
        var posToAdd = dir.normalized * _settings.SuckPosIncrease;
        var centerOfSuck = _rbs.LastElement().position + posToAdd;
        
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(centerOfSuck, _settings.SuckRadius, layerMask);

        for (int i = 0; i < collider2Ds.Length; i++)
        {
            
            var forceVec = collider2Ds[i].attachedRigidbody.position - sucker.position;
            var forceMult = Mathf.Lerp(_settings.MinSuck, _settings.MaxSuck, forceVec.magnitude / _settings.SuckRadius);
            var forceToApply = -(input * forceMult * forceVec);

            collider2Ds[i].attachedRigidbody.AddForce(forceToApply);
            Debug.DrawLine(centerOfSuck, collider2Ds[i].transform.position, new Color(1,1-forceMult/_settings.MaxSuck,0,1));
        }
    }

    private void UpdateVaccumSpriteDirections()
    {
        for (int i = 0; i < _rbs.Count-1; i++)
        {
            var p1 = _rbs[i].position;
            var p2 = _rbs[i+1].position;
            var dir = p1.DirectionTo(p2);
            float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            _rbs[i].rotation = zAngle + ALIGN_VERTICALLY;
        }
    }

    public void RotateVaccumHead(Vector2 dir)
    {
        if (dir.sqrMagnitude > 0.125f)
        {
            float targetZ = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 90f;
            float currentZ = _rbs.LastElement().rotation;
            float zRot = Mathf.LerpAngle(currentZ, targetZ, _settings.VaccumeHeadRotatePercent);
            _rbs.LastElement().rotation = zRot;
        }

    }
    
    
}
