using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using Legs;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
//responsible for 
public class LegFactory : MonoBehaviour
{
    [Range(0, 100)] public int LegLength = 20;
     private bool _isGrabbing; //WARNING: THIS IS ATTATCHED TO AN EVENT WHICH IS NEVER UNREJISTERED (when grabber is changed)
    [SerializeField] private SLegSettings _settings;
    [SerializeField] private GameObjectPool _pool;
    [SerializeField] private Color _color = Color.white;
    [SerializeField] private Material _lineRendererMaterial;
    private Rigidbody2D _rigidbody2D;
    private LineRenderer _lineRenderer;

    private List<Segment> _segs;
    private List<Rigidbody2D> _rbs;
    private List<SpriteRenderer> _segRenderers;
    private const float ALIGN_VERTICALLY = -90f;

    void Start()
    {
        _lineRenderer = new GameObject("Leg Factory Line Renderer").AddComponent<LineRenderer>();
        _lineRenderer.startColor = _color;
        _lineRenderer.endColor = _color;
        _lineRenderer.positionCount = LegLength;
        _lineRenderer.widthCurve = _settings.ScaleCurve;
        _lineRenderer.widthMultiplier = _settings.WidthMultiplier;
        _lineRenderer.material = _lineRendererMaterial;
        //set depth
        _lineRenderer.transform.position = new Vector3(0,0f,-20f);
        
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _segs = new List<Segment>(LegLength);
        _rbs = new List<Rigidbody2D>(LegLength);
        _segRenderers = new List<SpriteRenderer>(LegLength);

        //spawn initial leg attached to leg factory
        Segment prevSeg;
        SpringJoint2D prevJnt;
        Rigidbody2D prevRb;
        SpriteRenderer prevSprite;

        CreateNewSegment(0f, _rigidbody2D, out prevSeg, out prevJnt, out prevRb, out prevSprite);

        _segs.Add(prevSeg);
        _rbs.Add(prevRb);
        _segRenderers.Add(prevSprite);

        for (int i = 1; i < LegLength; i++) //spawn rest of legs
        {
            Segment currentSeg;
            SpringJoint2D currentJnt;
            Rigidbody2D currentRb;
            SpriteRenderer currentSpriteRenderer;
            
            float normIndex = (float) i / (LegLength-1);

            CreateNewSegment(normIndex, prevRb, out currentSeg, out currentJnt, out currentRb, out currentSpriteRenderer);

            prevSeg.Next = currentSeg;
            currentSeg.Previous = prevSeg;
            
            _segs.Add(currentSeg);
            _rbs.Add(currentRb);
            _segRenderers.Add(currentSpriteRenderer);
            
            prevJnt = currentJnt;
            prevRb = currentRb;
            prevSeg = currentSeg;

            if (i == LegLength - 1) //make last seg new grabber
            {
                MakeSegmentEndOfVaccum(currentSeg.gameObject);
                currentSpriteRenderer.sprite = _settings.VaccumTipSprite;
            }

        }
    }

    void CreateNewSegment(float normalizedIndex, in Rigidbody2D toAttachRb, out Segment seg, out SpringJoint2D jnt,
        out Rigidbody2D rb, out SpriteRenderer sprRenderer)
    {
        var current = _pool.Spawn();

        seg = current.GetComponent<Segment>();
        jnt = current.GetComponent<SpringJoint2D>();
        rb = current.GetComponent<Rigidbody2D>();
        sprRenderer = current.GetComponent<SpriteRenderer>();
        
        float deltaY = (_settings.ScaleCurve.Evaluate(normalizedIndex) * _settings.JointDistance)/2;
        seg.transform.position = toAttachRb.gameObject.transform.position + new Vector3(0, deltaY, 0);
        jnt.connectedBody = toAttachRb;

        sprRenderer.color = _color;
        sprRenderer.sprite = _settings.VaccumSegmentSprite;
        
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

    public void MoveLegs(Vector2 input)
    {

        for (int i = 0; i < _rbs.Count; i++)
        {
            float normalizedIndex = (float) i / (_rbs.Count - 1);
            _rbs[i].AddForce(_settings.OpenMaxForce * _settings.OpenForceCurve.Evaluate(normalizedIndex) * input);

        }
    }

    private void UpdateVaccumSpriteDirections()
    {
        for (int i = 0; i < _segRenderers.Count-1; i++)
        {
            var p1 = _segRenderers[i].transform.position;
            var p2 = _segRenderers[i+1].transform.position;
            var dir = p1.DirectionTo(p2);
            float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            _segRenderers[i].transform.rotation = Quaternion.Euler(new Vector3(0f,0f,zAngle + ALIGN_VERTICALLY));
        }
        {
            //for last seg
            var p1 = _segRenderers[_segRenderers.Count-2].transform.position;
            var p2 = _segRenderers[_segRenderers.Count-1].transform.position;
            var dir = p1.DirectionTo(p2);
            float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            _segRenderers[_segRenderers.Count-1].transform.rotation = Quaternion.Euler(new Vector3(0f,0f,zAngle + ALIGN_VERTICALLY));
        }
    }

    Blower MakeSegmentEndOfVaccum(GameObject gameObject)
    {
        var blower = gameObject.AddComponent<Blower>();
        blower.Settings = _settings;
        //blower.GetComponent<SpriteRenderer>().sprite = _blowerTipSprite;
        return blower;
    }
    
}
