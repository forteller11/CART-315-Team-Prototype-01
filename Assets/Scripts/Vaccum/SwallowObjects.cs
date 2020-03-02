using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using Legs;
using UnityEngine;
using Object = System.Object;

public class SwallowObjects : MonoBehaviour
{
    [SerializeField] private float WasteRotateOnVaccumAmount = 1f;
    [SerializeField] private float RelativeScaleChangeOnSwallow = 0.5f;
    [SerializeField] private float RelativeMassChangeOnSwallow = 0.1f;
    
    private int _sortingLayerInsideVaccum;
    private ControlsMaster _input;
    private float _inputValue;
    private List<Swallowable> _swallowedObjects = new List<Swallowable>(12);

    private void Awake()
    {
        _input = new ControlsMaster();
        _sortingLayerInsideVaccum = SortingLayer.NameToID("BehindVaccum");
    } 
    private void OnEnable() => _input.Enable(); 
    private void OnDisable() => _input.Disable();


    void OnTriggerEnter2D(Collider2D other)
    {
        var swallowed = other.GetComponent<Swallowable>();
        if ((swallowed == null) || (_inputValue == 0))
            return;
        
        swallowed.Rigidbody2D.position = VaccumBodySingleton.Instance.transform.position.To2DIgnoreZ();
        swallowed.gameObject.layer = LayerMask.NameToLayer("InsideVaccum");
        swallowed.GetComponent<SpriteRenderer>().sortingLayerID = _sortingLayerInsideVaccum;
        swallowed.Rigidbody2D.mass *= RelativeMassChangeOnSwallow;
        swallowed.transform.localScale *= RelativeScaleChangeOnSwallow;
        _swallowedObjects.Add(swallowed);

    }

    private void Update()
    {
        _inputValue = _input.Vaccum.Suck.ReadValue<float>();

        //spin objects in vaccum around if sucking
        if (_inputValue > 0f)
        {
            Vector2 centerPos = VaccumBodySingleton.Instance.transform.position.To2DIgnoreZ();
            for (int i = 0; i < _swallowedObjects.Count; i++)
            {
                var toRigidBody = centerPos.DirectionTo(_swallowedObjects[i].Rigidbody2D.position);
                var forceDir = new Vector2(-toRigidBody.y, toRigidBody.x); //clockwise
                var forceMult = _inputValue * WasteRotateOnVaccumAmount;
                var force = forceDir * forceMult;
                _swallowedObjects[i].Rigidbody2D.AddForce(force);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
            Gizmos.DrawWireSphere(VaccumBodySingleton.Instance.transform.position.To2DIgnoreZ(), 0.2f);
    }
    
}
