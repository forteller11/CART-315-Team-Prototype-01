using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
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


        private Queue<IPoolable> _pool;

        private void Awake()
        {
            if (transform.position != Vector3.zero)
                Debug.LogWarning($"Pool \"{gameObject.name}\" transform not set to (0,0,0)! May cause positional problems!");
            
            
            _pool = new Queue<IPoolable>(_initialCapacity);
            for (int i = 0; i < _initialCapacity; i++)
            {
                _pool.Enqueue(CreateNewObject($"{_prefab.name} {++_numberOfObjs}"));
                
            }
        }
        
        IPoolable CreateNewObject(in string name)
        {
            var newObj = Instantiate(_prefab, Vector3.zero, Quaternion.identity);
            newObj.name = name;
            newObj.transform.SetParent(transform);
            
            var poolObj = newObj.GetFirstInterface<IPoolable>();
            if (poolObj == null) Debug.LogError("this prefab contains no IPoolable interfaces!");
            poolObj.ReturnToPool = OnReturnToPool;
            poolObj.PooledGameObject = newObj;
            poolObj.SetActiveTo(false);
            
            
            return poolObj;
        }

        public GameObject Spawn()
        {
            if (_pool.Count == 0)
                _pool.Enqueue(CreateNewObject($"{_prefab.name} {++_numberOfObjs} (added)"));
            
            var newSeg = _pool.Dequeue();
            newSeg.SetActiveTo(true);
            return newSeg.PooledGameObject;
        }
        
        private void OnReturnToPool(IPoolable toPool)
        {
            toPool.SetActiveTo(false);
            _pool.Enqueue(toPool);
            toPool.PooledGameObject.transform.SetParent(transform);
        }
    }


}
