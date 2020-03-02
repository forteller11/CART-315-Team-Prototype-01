using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class BlowPowerUp : MonoBehaviour
    {
        private static float TIME_TO_BE_ACTIVE = 200f;
        private float _timeSinceAwake;

        private void Update()
        {
            _timeSinceAwake += Time.deltaTime;
            if (_timeSinceAwake > TIME_TO_BE_ACTIVE)
                Destroy(this);
        }
    }
}