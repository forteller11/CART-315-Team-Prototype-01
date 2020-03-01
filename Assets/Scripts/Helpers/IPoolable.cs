using System;
using UnityEngine;

namespace Helpers
{
    public interface IPoolable
    {
        GameObjectPool Pool { get; set; }
    }
}