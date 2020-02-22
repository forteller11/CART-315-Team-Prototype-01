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
    private Rigidbody2D _rigidbody2D;
    private Blower _blower;
    private LineRenderer _lr;
    private List<GameObject> _vaccumVisualizers;

    private List<Segment> _segs;
    private List<Rigidbody2D> _rbs;

    void Start()
    {
        if (LegLength % 2 != 0)
            Debug.LogError("leg length must be multiple of 2!");
        
        _lr = gameObject.AddComponent<LineRenderer>();
        _lr.startColor = _color;
        _lr.endColor = _color;
        _lr.positionCount = LegLength;
        _lr.widthCurve = _settings.ScaleCurve;
        _lr.widthMultiplier = _settings.WidthMultiplier;
        _lr.material = new Material(Shader.Find("Unlit/Texture"));
        _lr.transform.position = new Vector3(0,0f,-20f);
        
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _segs = new List<Segment>(LegLength);
        _rbs = new List<Rigidbody2D>(LegLength);

        _vaccumVisualizers = new List<GameObject>(LegLength / 2);
        for (int i = 0; i < LegLength; i++)
        {
            _vaccumVisualizers.Add(new GameObject($"Leg Visualizer {i}"));
            var sprRenderer = _vaccumVisualizers[i].AddComponent<SpriteRenderer>();
            if (i != LegLength - 1)
                sprRenderer.sprite = _settings.VaccumSegmentSprite;
            else
                sprRenderer.sprite = _settings.VaccumTipSprite;
        }
        
        //spawn initial leg attached to leg factory
        Segment prevSeg;
        SpringJoint2D prevJnt;
        Rigidbody2D prevRb;

        CreateNewSegment(0f, _rigidbody2D, out prevSeg, out prevJnt, out prevRb);

        _segs.Add(prevSeg);
        _rbs.Add(prevRb);

        for (int i = 1; i < LegLength; i++) //spawn rest of legs
        {
            Segment currentSeg;
            SpringJoint2D currentJnt;
            Rigidbody2D currentRb;

            float normIndex = (float) i / (LegLength-1);

            CreateNewSegment(normIndex, prevRb, out currentSeg, out currentJnt, out currentRb);

            prevSeg.Next = currentSeg;
            currentSeg.Previous = prevSeg;
            
            _segs.Add(currentSeg);
            _rbs.Add(currentRb);
            
            prevJnt = currentJnt;
            prevRb = currentRb;
            prevSeg = currentSeg;

            if (i == LegLength - 1) //make last seg new grabber
            {
                _blower = MakeSegmentEndOfVaccum(currentSeg.gameObject);
            }

        }
    }

    void CreateNewSegment(float normalizedIndex, in Rigidbody2D toAttachRb, out Segment seg, out SpringJoint2D jnt,
        out Rigidbody2D rb)
    {
        var current = _pool.Spawn();

        seg = current.GetComponent<Segment>();
        jnt = current.GetComponent<SpringJoint2D>();
        rb = current.GetComponent<Rigidbody2D>();

        float deltaY = (_settings.ScaleCurve.Evaluate(normalizedIndex - 0.5f) * _settings.JointSpacing);
        seg.transform.position = toAttachRb.gameObject.transform.position - new Vector3(0, deltaY, 0);
        jnt.connectedBody = toAttachRb;

        var spr = current.GetComponent<SpriteRenderer>();
        if (spr != null)
            spr.color = _color;

        jnt.autoConfigureDistance = true;
        jnt.distance = _settings.JointDistance * _settings.ScaleCurve.Evaluate(normalizedIndex)/2;
        jnt.frequency = _settings.MaxFrequency * _settings.FrequencyCurve.Evaluate(normalizedIndex);
        jnt.dampingRatio = _settings.MaxDampening * _settings.DampeningCurve.Evaluate(normalizedIndex);

        rb.gravityScale = _settings.MaxGravity * _settings.GravityCurve.Evaluate(normalizedIndex);
        rb.mass = _settings.MaxMass * _settings.MassCurve.Evaluate(normalizedIndex);

        current.transform.localScale = _settings.ScaleCurve.Evaluate(normalizedIndex) * _settings.MaxScale;

    }

    void Update()
    {
        for (int i = 0; i < _rbs.Count; i++)
            _lr.SetPosition(i, _rbs[i].transform.position);
        
        UpdateVaccumSpritePositions();
    }

    public void MoveLegs(Vector2 input)
    {

        for (int i = 0; i < _rbs.Count; i++)
        {
            float normalizedIndex = (float) i / (_rbs.Count - 1);
            _rbs[i].AddForce(_settings.OpenMaxForce * _settings.OpenForceCurve.Evaluate(normalizedIndex) * input);

        }
    }

    private void UpdateVaccumSpritePositions()
    {
        for (int i = 0; i < _rbs.Count-1; i+=2)
        {
            //get direction from seg1 --> seg2
            int index = i / 2;
            var dirVecBetween = Vector3.Normalize(_rbs[i].transform.position - _rbs[i+1].transform.position);
            float rotZ = (Mathf.Atan2(dirVecBetween.y, dirVecBetween.x) * Mathf.Rad2Deg) - 90f;
            _vaccumVisualizers[index].transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
            
            //mean position
            _vaccumVisualizers[index].transform.position = Vector3.Lerp(_rbs[i].transform.position, _rbs[i+1].transform.position, 0.5f);
        }
    }
//    private void AsignBlowerDirection()
//    {
//        var rbLast = _rbs[_rbs.Count - 1];
//        var rb2ndLast = _rbs[_rbs.Count - 2];
//
//        var legDir = Vector3.Normalize(rbLast.transform.position - rb2ndLast.transform.position);
//        _blower.LegTipDir = legDir;
//    }

    Blower MakeSegmentEndOfVaccum(GameObject gameObject)
    {
        var blower = gameObject.AddComponent<Blower>();
        blower.Settings = _settings;
        //blower.GetComponent<SpriteRenderer>().sprite = _blowerTipSprite;
        return blower;
    }
    
}
