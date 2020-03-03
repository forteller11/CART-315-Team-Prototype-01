using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using Legs;
using Unity.Mathematics;
using UnityEngine;

public class SuckObjects : MonoBehaviour
{
    public float SuckMultiplier = 1f;
    [SerializeField] private LayerMask LayersThatCanBlockSuck;


    [SerializeField] private VaccumSettingsScriptable _settings;
    
    private ControlsMaster _input;
    private float _inputValue;
    [SerializeField] private LayerMask _layersToSuck;

    private void Awake()
    {
        _input = new ControlsMaster();
        if (_layersToSuck == 0)
            Debug.LogWarning("Layers To Suck as not been set, which may cause problems!");
    }

    private void OnEnable() => _input.Enable(); 
    private void OnDisable() => _input.Disable(); 
    private void Update() => _inputValue = _input.Vaccum.Suck.ReadValue<float>();
    
    //check all collisions within cone
    private void OnTriggerStay2D(Collider2D other)
    {
        //if not of correct layer
        if ((1 << other.gameObject.layer) != _layersToSuck.value)
            return;
        if (_inputValue == 0)
            return;
        
        var forceDir = other.attachedRigidbody.position - transform.position.To2DIgnoreZ();
        var forceDirMag = forceDir.magnitude;
        
        //check if something is inbetween object and sucker, if so, don't suck'
        var posOrigin = transform.position.To2DIgnoreZ();
        var dir = posOrigin.DirectionTo(other.attachedRigidbody.position);
        RaycastHit2D hit = Physics2D.Raycast(posOrigin, dir, distance: forceDirMag, LayersThatCanBlockSuck.value);
        if (hit.collider == null)
        {

            float curveIndex = 1 - (forceDirMag / _settings.SuckRadius);
            var forceMult = _settings.SuckCurve.Evaluate(curveIndex) * _settings.MaxSuck * SuckMultiplier;

            var forceToApply = -(_inputValue * forceMult * forceDir);

            other.attachedRigidbody.AddForce(forceToApply);
            Debug.DrawLine(this.transform.position, other.transform.position, new Color(1, 1, 0, curveIndex/2f + 0.5f));
        }
        else 
            Debug.DrawLine(posOrigin, hit.point, Color.red);
    }

    private void OnDrawGizmos()
    {

        Gizmos.DrawWireSphere(transform.position, _settings.SuckRadius);
    }
}
