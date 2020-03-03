using System;
using System.Collections;
using System.Timers;
using UnityEngine;
using Object = UnityEngine.Object;
using UnityEngine.UI;

namespace Legs
{
    public class CombineManager : MonoBehaviour
    {
        private SuckObjects SuckObjects;
        [SerializeField] private float SuckIncreaseOnEnergyCollision = 2f;
        [SerializeField] private float SuckIncreaseTimeOnEnergyCollision = 2; //2s

        private Text comboText;

        void Start()
        {
            SuckObjects = GameObject.Find("SuckObjects").GetComponent<SuckObjects>();
            if (SuckObjects == null)
                Debug.LogError("Suck Objects not found, has the name of the prefab been changed?");
            comboText = GameObject.Find("Combo Heading").GetComponent<Text>();
        }
        public void OnCombination(Swallowable s1, Swallowable s2)
        {
            Debug.Log("on combo");
            var combinedType = s1.Type | s2.Type;

            //Add Functionality here
            switch (combinedType)
            {
                //if a liquid and energy type combine...
                case Swallowable.CombinableType.Liquid | Swallowable.CombinableType.Energy:
                    VaccumBodySingleton.Instance.GetComponent<SpriteRenderer>().color = Color.red;
                    s1.Pool.ReturnToPool(s1.gameObject);
                    s2.Pool.ReturnToPool(s2.gameObject);
                    comboText.text = "Combo: Liquid + Energy";
                    //TODO start coroutine to force multiplier in settings.... at end it changes it back
                    break;
            
                //if a clothe and energy type combine...
                case Swallowable.CombinableType.Energy | Swallowable.CombinableType.Energy:
                    s1.Pool.ReturnToPool(s1.gameObject);
                    s2.Pool.ReturnToPool(s2.gameObject);
                    SuckObjects.PowerUpSuckMultiplier = SuckIncreaseOnEnergyCollision;
                    comboText.text = "Combo: Energy + Energy";
                    // TODO create particle system to show change
                    StopCoroutine("ResetSuckForce");
                    StartCoroutine("ResetSuckForce");
                    break;
            }
        }

        IEnumerator ResetSuckForce()
        {
            yield return new WaitForSeconds(SuckIncreaseTimeOnEnergyCollision);
            SuckObjects.PowerUpSuckMultiplier = 1f;
            
            Debug.Log("back to normal");
        }
    }
}