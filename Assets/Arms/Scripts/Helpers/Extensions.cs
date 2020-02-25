using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

namespace Helpers
{
    public static class Extensions
    {
        public static int WrapAroundList<T>(this int index, List<T> list)
        {
            if (index > list.Count - 1)
                index = 0;
            if (index < 0)
                index = list.Count - 1;
            return index;
        }
        
        
        public static List<T> ReturnAllObjectsWithComponent<T>(this RaycastHit[] hits) where T : MonoBehaviour
        {
            var objsOfType = new List<T>();

            foreach (var hit in hits)
            {
                T script = hit.transform.GetComponent<T>();
                if (script != null)
                    objsOfType.Add(script);
            }

            return objsOfType;
        }

        public static T ReturnFirstObjectWithComponent<T> (this RaycastHit[] hits) where T : MonoBehaviour
        {
            foreach (var hit in hits)
            {
                T script = hit.transform.GetComponent<T>();
                if (script != null)
                    return script;
            }
            return null;
        }
        
        //DOESN'T WORK PROPERLY, (doesn't return nearest atm)
        public static T ReturnClosestWithComponent<T> (this RaycastHit[] hits, Vector3 pos) where T : MonoBehaviour
        {
            T closestScript = null;
            float distanceOfClosestScript = Mathf.Infinity;
            for (int i = 0; i < hits.Length; i++)
            {
                T currentScript = hits[i].transform.GetComponent<T>();
                if (currentScript != null)
                {
                    float distanceOfCurrentScript = currentScript.transform.position.DistanceToSquared(pos);
                    if (distanceOfCurrentScript < distanceOfClosestScript)
                    {
                        distanceOfClosestScript = distanceOfCurrentScript;
                        closestScript = currentScript;
                    }
                }
            }

            return closestScript;
        }
        
        public static float DistanceToSquared(this Vector3 v1, Vector3 v2)
        {
            var delta = v1 - v2;
            return Vector3.SqrMagnitude(delta);
        }
        
        public static List<T> GetInterfaces<T>(this GameObject go) where T : class
        {
            List<T> interfaces = new List<T>();
            MonoBehaviour[] scripts = go.GetComponents<MonoBehaviour>();
            foreach (var script in scripts)
            {
                if (script is T)
                {
                    interfaces.Add(script as T);
                }
            }
            return interfaces;
        }
        
        public static T GetFirstInterface<T>(this GameObject go) where T : class
        {
            MonoBehaviour[] scripts = go.GetComponents<MonoBehaviour>();
            foreach (var script in scripts)
            {
                if (script is T)
                {
                    return script as T;
                }
            }

            return null;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ToVector3XY(this Vector2 vec2)
        {
            return new Vector3(vec2.x, vec2.y, 0f);
        }
        
        /// <summary>
        /// returns a unit vector pointing from v1 --> v2
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 DirectionTo(this Vector3 v1, Vector3 v2)
        {
            return Vector3.Normalize(v2 - v1);
        }


    }
}