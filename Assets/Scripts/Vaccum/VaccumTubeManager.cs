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

//responsible for spawning and controlling vaccum tubes and tip/sucker
public class VaccumTubeManager : MonoBehaviour
{
    [Range(0, 100)] private int TubeLength = 20;
    [SerializeField] private VaccumSettingsScriptable SettingsScriptable;
    [SerializeField] private GameObjectPool LegPool;
    [SerializeField] private Material LineRendererMaterial;
    
    private Rigidbody2D _rigidbody2D;
    private LineRenderer _lineRenderer;
    private ControlsMaster _input;
    private List<Rigidbody2D> _rbs;
    private const float ALIGN_VERTICALLY = -90f;
    private Suction _suction;

    
    void Start()
    {
        _lineRenderer = new GameObject("Leg Factory Line Renderer").AddComponent<LineRenderer>();
        _lineRenderer.startColor = Color.white;
        _lineRenderer.endColor = Color.white;
        _lineRenderer.positionCount = TubeLength;
        _lineRenderer.widthCurve = SettingsScriptable.ScaleCurve;
        _lineRenderer.widthMultiplier = SettingsScriptable.WidthMultiplier;
        _lineRenderer.material = LineRendererMaterial;
        _lineRenderer.material.color = SettingsScriptable.VaccumSegmentLineColor;
        _lineRenderer.transform.position = new Vector3(0,0f,-20f);
        
        //spawn initial leg attached to leg factory
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _rbs = new List<Rigidbody2D>(TubeLength);
        
        var first = LegPool.Spawn();
        first.GetComponent<SpriteRenderer>().color = SettingsScriptable.VaccumSegmentTint;
        
        SpringJoint2D prevJnt;
        Rigidbody2D prevRb;

        CreateNewSegment(0f, first, _rigidbody2D, out prevJnt, out prevRb);

        _rbs.Add(prevRb);

        for (int i = 1; i < TubeLength; i++) //spawn rest of legs
        {
            Tube currentSeg;
            SpringJoint2D currentJnt;
            Rigidbody2D currentRb;

            float normIndex = (float) i / (TubeLength-1);
            GameObject current;
            if (i != TubeLength - 1)
            {
                current = LegPool.Spawn();
                current.GetComponent<SpriteRenderer>().color = SettingsScriptable.VaccumSegmentTint;
            }
            else //if the last segment, spawn the sucker
            {
                current = Instantiate(SettingsScriptable.VaccumTipPrefab);
                current.name = $" {gameObject.name}'s Sucker";
                current.GetComponent<SpriteRenderer>().color = SettingsScriptable.VaccumTipTint;
                _suction = current.GetComponent<Suction>();
                if (_suction == null) Debug.LogError("_sucker not assigned, the prefab is probably not configured properly!");
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

        float deltaY = (SettingsScriptable.ScaleCurve.Evaluate(normalizedIndex) * SettingsScriptable.JointDistance)/2;
        rb.position = toAttachRb.position + new Vector2(0, deltaY);
        jnt.connectedBody = toAttachRb;

        jnt.autoConfigureDistance = false;
        jnt.distance = SettingsScriptable.JointDistance/2 * SettingsScriptable.ScaleCurve.Evaluate(normalizedIndex);
        jnt.frequency = SettingsScriptable.MaxFrequency * SettingsScriptable.FrequencyCurve.Evaluate(normalizedIndex);
        jnt.dampingRatio = SettingsScriptable.MaxDampening * SettingsScriptable.DampeningCurve.Evaluate(normalizedIndex);
        //jnt.enableCollision = false;
        
        rb.gravityScale = SettingsScriptable.MaxGravity * SettingsScriptable.GravityCurve.Evaluate(normalizedIndex);
        rb.mass = SettingsScriptable.MaxMass * SettingsScriptable.MassCurve.Evaluate(normalizedIndex);

        current.transform.localScale = SettingsScriptable.ScaleCurve.Evaluate(normalizedIndex) * SettingsScriptable.MaxScale;

    }
    
    void Awake() => _input = new ControlsMaster();
    private void OnEnable() => _input.Enable(); 
    private void OnDisable() => _input.Disable();

    void Update()
    {
        MoveLegs(_input.Vaccum.MoveTube.ReadValue<Vector2>(), _input.Vaccum.Suck.ReadValue<float>());
        RotateVaccumHead(_input.Vaccum.RotateHead.ReadValue<Vector2>());
        for (int i = 0; i < _rbs.Count; i++)
            _lineRenderer.SetPosition(i, _rbs[i].transform.position);
        UpdateVaccumSpriteDirections();
    }


    public void MoveLegs(Vector2 inputMove, float inputSuck)
    {

        var blowerRB = _rbs.LastElement();
        
        Vector2[] collisionSuctionPoints = _suction.GetSuctionPoints();

        //where 1 == full suction, 0 == no suction
        //amount walll is being sucked vs arm being moved, determined by input and whether close to suctionable (layer-mask) rigidbody
        float suctionAmountNorm = ((float) collisionSuctionPoints.Length / _suction.SuctionCollidersCount) * inputSuck;
        
        blowerRB.angularDrag = Mathf.Lerp(SettingsScriptable.AngularDampeningOnNoSuction, SettingsScriptable.AngularDampeningOnSuction, suctionAmountNorm);

        //apply suction forces
        float maxForceForEachSuctionPoint = (float) SettingsScriptable.MaxSuctionForce / _suction.SuctionCollidersCount;
        for (int i = 0; i < collisionSuctionPoints.Length; i++)
        {
            var towardsSuction = collisionSuctionPoints[i] - blowerRB.position;
            float forceMult = suctionAmountNorm * maxForceForEachSuctionPoint;
            var forceToApply = towardsSuction * forceMult;

            blowerRB.AddForce(forceToApply);
            Debug.DrawLine(blowerRB.position, collisionSuctionPoints[i], new Color(.5f, .5f, 1, inputSuck));
        }

        //force on arms (not suction)
        for (int i = 0; i < _rbs.Count; i++)
        {
            float indexNorm = (float) i / (_rbs.Count - 1);
            float forceMult = (1 - suctionAmountNorm) * SettingsScriptable.OpenMaxForce * SettingsScriptable.OpenForceCurve.Evaluate(indexNorm);
            var forceToAddUnclamped = forceMult * inputMove;

            _rbs[i].AddForce(forceToAddUnclamped);
            _rigidbody2D.AddForce(-forceToAddUnclamped); //so can't float/ drag yourself forward like superman
            //clamp current vel, not add force
            float mag = _rbs[i].velocity.magnitude;
            if (mag > SettingsScriptable.MaxTubeVelocity)
            {
                float percentageToMaxMag =  SettingsScriptable.MaxTubeVelocity / mag;
                _rbs[i].velocity *= percentageToMaxMag;
            }

        }

        { //force to body (if suction)
            Vector2 forceToApply = SettingsScriptable.ForceOnBody * suctionAmountNorm * inputMove;
            _rigidbody2D.AddForce(forceToApply);
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
            float zRot = Mathf.LerpAngle(currentZ, targetZ, SettingsScriptable.VaccumeHeadRotatePercent);
            _rbs.LastElement().rotation = zRot;
        }

    }
    
    
}
