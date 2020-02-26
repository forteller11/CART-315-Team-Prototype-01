using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

//Created using the following resources as reference
//Jason Weimann's "Object Pooling" : https://www.youtube.com/watch?v=uxm4a0QnQ9E
//Sam Izzo's "Type-safe object pool for Unity" : https://www.gamasutra.com/blogs/SamIzzo/20180611/319671/Typesafe_object_pool_for_Unity.php

namespace Helpers
{
    public class GameObjectPool : MonoBehaviour
    {
        [SerializeField]
        private int _initialCapacity = 40;
        [SerializeField]
        private GameObject _prefab;

        private int _numberOfObjs;


        private Queue<GameObject> _pool; //all inactive objects from pool
        private List<GameObject> _allObjectsFromPool;
        private bool _allActiveObjectsFromPoolIsDirty = true;
        private List<GameObject> _allActiveObjectsFromPool;
        public List<GameObject> AllActiveObjectsFromPool
        {
            get
            {
                if (_allActiveObjectsFromPoolIsDirty)
                {
                    _allActiveObjectsFromPool = new List<GameObject>();
                    for (int i = 0; i < _allActiveObjectsFromPool.Count; i++)
                    {
                        if (_allActiveObjectsFromPool[i].activeSelf) 
                            _allObjectsFromPool.Add(_allActiveObjectsFromPool[i]);
                    }

                    _allActiveObjectsFromPoolIsDirty = false;
                }

                return _allActiveObjectsFromPool;

            }
        }

        private void Awake()
        {
            if (transform.position != Vector3.zero)
                Debug.LogWarning($"Pool \"{gameObject.name}\" transform not set to (0,0,0)! May cause positional problems!");
            
            
            _pool = new Queue<GameObject>(_initialCapacity);
            _allObjectsFromPool = new List<GameObject>(_initialCapacity);
            for (int i = 0; i < _initialCapacity; i++)
            {
                _pool.Enqueue(CreateNewObject($"{_prefab.name} {++_numberOfObjs}"));
            }
        }
        
        GameObject CreateNewObject(in string name)
        {
            var newObj = Instantiate(_prefab, Vector3.zero, Quaternion.identity);
            newObj.name = name;
            newObj.transform.SetParent(transform);
            newObj.SetActive(false);
            
            _allObjectsFromPool.Add(newObj);
            
            var poolObj = newObj.GetFirstInterface<IPoolable>();
            if (poolObj == null) Debug.LogError("This prefab doesn't contain an IPoolable interface!");
            poolObj.Pool = this;

            _allActiveObjectsFromPoolIsDirty = true;
                
            return newObj;
        }

        public GameObject Spawn()
        {
            if (_pool.Count == 0) //if need to expand pool capcity
            {
                _pool.Enqueue(CreateNewObject($"{_prefab.name} {++_numberOfObjs} (expanded)"));
                Debug.LogWarning($"{gameObject.name} pool has been expanded! Consider increasing the initial capacity of the pool for optimal performance.");
            }
            
            var newObj = _pool.Dequeue();
            newObj.SetActive(true);
            _allActiveObjectsFromPoolIsDirty = true;
            return newObj;
        }
        
        public void ReturnToPool(GameObject toPool)
        {
            _allActiveObjectsFromPoolIsDirty = true;
            toPool.SetActive(false);
            _pool.Enqueue(toPool);
            toPool.transform.SetParent(transform);
        }
    }


}
