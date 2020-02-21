using System;
using UnityEngine;

namespace Helpers
{
    public interface IPoolable
    {
        void SetActiveTo(bool trueFalse);
        Action<IPoolable> ReturnToPool { get; set; }
        GameObject PooledGameObject { get; set; }
    }
}