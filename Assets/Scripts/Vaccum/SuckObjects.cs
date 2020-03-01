using System;
using System.Collections;
using System.Collections.Generic;
using Legs;
using UnityEngine;

public class SuckObjects : MonoBehaviour
{
    [SerializeField] private Vector2 _centerOfSuckOffset;

    private Vector2 CenterOfSuck
    {
        get => _centerOfSuckOffset + new Vector2(transform.position.x, transform.position.y); 
    }
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
 
        var forceVec = other.attachedRigidbody.position - CenterOfSuck;
        float curveIndex = 1-(forceVec.magnitude / _settings.SuckRadius);
        var forceMult = _settings.SuckCurve.Evaluate(curveIndex) * _settings.MaxSuck;
  
        var forceToApply = -(_inputValue * forceMult * forceVec);

        other.attachedRigidbody.AddForce(forceToApply);
        Debug.DrawLine(CenterOfSuck, other.transform.position, new Color(1,curveIndex,0,1));
    }

    private void OnDrawGizmos()
    {

        Gizmos.DrawWireSphere(CenterOfSuck, _settings.SuckRadius);
    }
}
