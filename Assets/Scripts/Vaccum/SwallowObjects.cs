using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using Legs;
using UnityEngine;
using Object = System.Object;

public class SwallowObjects : MonoBehaviour
{
    [SerializeField] private Vector2 WasteSpawnOffset;

    private GameObjectPool _wastePool;
    private ControlsMaster _input;
    private float _inputValue;
    private List<SwallowedObject> _swallowedObjects = new List<SwallowedObject>();
    private List<GameObject> _wastes = new List<GameObject>(2);

    private void Awake()
    {
        _input = new ControlsMaster();
        _wastePool = GetComponent<GameObjectPool>();
    } 
    private void OnEnable() => _input.Enable(); 
    private void OnDisable() => _input.Disable();


    void OnTriggerEnter2D(Collider2D other)
    {
        var suckable = other.GetComponent<Suckable>();
        if ((suckable == null) || (_inputValue <= 0))
            return;
        
        _swallowedObjects.Add(new SwallowedObject(suckable));
        var newWasteRB = _wastePool.Spawn().GetComponent<Rigidbody2D>();
        newWasteRB.position = VaccumBodySingleton.Instance.transform.position.To2DIgnoreZ() + WasteSpawnOffset;
        _wastes.Add(newWasteRB.gameObject);
        
        suckable.Pool.ReturnToPool(suckable.gameObject);
    }

    private void Update()
    {
        _inputValue = _input.Vaccum.Suck.ReadValue<float>();

//        if (_inputValue == 0f)
//        {
//            for (int i = 0; i < _swallowedObjects.Count; i++)
//            {
//                if (_swallowedObjects[i].TimeSucked
//            }
//        }
        //check if any history should be removed
    }

    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
            Gizmos.DrawWireSphere(WasteSpawnOffset + VaccumBodySingleton.Instance.transform.position.To2DIgnoreZ(), 0.2f);
    }

    internal struct SwallowedObject
    {
        public Suckable.SuckableType SuckableType;
        public Sprite Sprite;
        public double TimeSucked;
        
        public SwallowedObject(Suckable suckable)
        {
            SuckableType = suckable.Type;
            Sprite = suckable.GetComponent<SpriteRenderer>().sprite;
            TimeSucked = Time.time;
        }
    }
}
