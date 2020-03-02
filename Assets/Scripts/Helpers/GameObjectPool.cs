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
        [Tooltip("All children of this pool will become incorporated into the pool. Make sure all children have the same components as the connect prefab!")]
        [SerializeField] private bool ConsiderChildrenAsPartOfPool = true;

        
        private int _numberOfObjs;


        private Queue<GameObject> _pool; //all inactive objects from pool
        private List<GameObject> _allObjectsFromPool;
        private bool _allActiveObjectsFromPoolIsDirty = true;
        private List<GameObject> _allActiveObjectsFromPool;
        //get all current active objects from pool
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
            
            //incorporate all children into pool
            if ( (transform.childCount > 0) && (ConsiderChildrenAsPartOfPool))
            {
                
                for (int i = 0; i < transform.childCount; i++)
                {
                    IncorporateIntoPool(transform.GetChild(i).gameObject);
                }
            }

            int newObjToSpawn = (ConsiderChildrenAsPartOfPool)
                ? _initialCapacity - transform.childCount
                : _initialCapacity;

                
            for (int i = 0; i < newObjToSpawn; i++)
            {
                _pool.Enqueue(CreateNewObject($"{_prefab.name} {++_numberOfObjs}"));
            }
        }
        
        GameObject CreateNewObject(in string name)
        {
            var newObj = Instantiate(_prefab, Vector3.zero, Quaternion.identity);
            newObj.name = name;
            //newObj.transform.SetParent(transform);
            newObj.SetActive(false);
            
            _allObjectsFromPool.Add(newObj);
            
            var poolObj = newObj.GetFirstInterface<IPoolable>();
            if (poolObj == null) 
                Debug.LogError("This prefab doesn't contain an IPoolable interface!");
            else 
                poolObj.Pool = this;

            _allActiveObjectsFromPoolIsDirty = true;
                
            return newObj;
        }
        
        void IncorporateIntoPool(GameObject toIncorporate)
        {
            toIncorporate.name = $"{toIncorporate.name} (incorporated) {++_numberOfObjs}";

            _allObjectsFromPool.Add(toIncorporate);
            
            var poolObj = toIncorporate.GetFirstInterface<IPoolable>();
            if (poolObj == null) 
                Debug.LogError($"{toIncorporate.name} doesn't contain an IPoolable interface!");
            else 
                poolObj.Pool = this;

            _allActiveObjectsFromPoolIsDirty = true;
   
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
            //toPool.transform.SetParent(transform);
        }
    }


}
