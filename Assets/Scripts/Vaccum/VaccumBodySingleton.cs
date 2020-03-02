using System;
using UnityEngine;

namespace Legs
{
    public class VaccumBodySingleton : MonoBehaviour
    {
        private static VaccumBodySingleton _instance = null;
        public static VaccumBodySingleton Instance
        {
            get
            {
                if (_instance == null)
                    Debug.LogError("No Objects in the scene have this singletonComponent!");
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance != null)
                Debug.LogError("There are multiple components in the scene with this singleton component!");
            
            _instance = this;
        }
    }
}