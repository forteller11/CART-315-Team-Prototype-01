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
    [SerializeField] private SLegSettings _settings;
    
    private InputGrabber _input;
    private float _inputValue;
    [SerializeField] private LayerMask _layerMask;

    private void Awake() => _input = new InputGrabber();
    private void OnEnable() => _input.Enable(); 
    private void OnDisable() => _input.Disable(); 
    private void Update() => _inputValue = _input.InGame.Suck.ReadValue<float>();
    
    private void OnTriggerStay2D(Collider2D other)
    {
        //check all collisions within cone

 
            var forceVec = other.attachedRigidbody.position - CenterOfSuck;
            var forceMult = Mathf.Lerp(_settings.MinSuck, _settings.MaxSuck, forceVec.magnitude / _settings.SuckRadius);
            var forceToApply = -(_inputValue * forceMult * forceVec);

            other.attachedRigidbody.AddForce(forceToApply);
            Debug.DrawLine(CenterOfSuck, other.transform.position, new Color(1,1-forceMult/_settings.MaxSuck,0,1));
    }

    private void OnDrawGizmos()
    {

        Gizmos.DrawWireSphere(CenterOfSuck, _settings.SuckRadius);
    }
}
