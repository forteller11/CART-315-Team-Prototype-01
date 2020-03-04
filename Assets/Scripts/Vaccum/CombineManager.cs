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
        private Text notificationText;

        GameObject UI;
        detectIfSucked suctionDetectionScript;

        void Start()
        {
            SuckObjects = GameObject.Find("SuckObjects").GetComponent<SuckObjects>();
            if (SuckObjects == null)
                Debug.LogError("Suck Objects not found, has the name of the prefab been changed?");

            comboText = GameObject.Find("Combo Heading").GetComponent<Text>();
            notificationText = GameObject.Find("Notification Heading").GetComponent<Text>();

            UI = GameObject.Find("UI");
            suctionDetectionScript = UI.GetComponent<detectIfSucked>();
            notificationText.text = "";
        }
        public void OnCombination(Swallowable s1, Swallowable s2)
        {
            Debug.Log("on combo");
            var combinedType = s1.Type | s2.Type;
            var currentColor = comboText.color;

            //Add Functionality here
            switch (combinedType)
            {
                //if a liquid and energy type combine...
                case Swallowable.CombinableType.Liquid | Swallowable.CombinableType.Energy:
                    VaccumBodySingleton.Instance.GetComponent<SpriteRenderer>().color = Color.red;
                    StopCoroutine("ResetColor");
                    StartCoroutine("ResetColor");
                    s1.Pool.ReturnToPool(s1.gameObject);
                    s2.Pool.ReturnToPool(s2.gameObject);
                    currentColor = Color.red;
                    comboText.color = currentColor;
                    comboText.text = "Liquid + Energy";
                    suctionDetectionScript.obj0Sucked = true; // energy 1
                    suctionDetectionScript.obj1Sucked = false; // energy 2
                    suctionDetectionScript.obj2Sucked = true; // liquid 
                    notificationText.text = "";
                    //TODO start coroutine to force multiplier in settings.... at end it changes it back
                    break;
            
                //if a energy and energy type combine...
                case Swallowable.CombinableType.Energy | Swallowable.CombinableType.Energy:
                    s1.Pool.ReturnToPool(s1.gameObject);
                    s2.Pool.ReturnToPool(s2.gameObject);
                    SuckObjects.PowerUpSuckMultiplier = SuckIncreaseOnEnergyCollision;
                    currentColor = Color.yellow;
                    comboText.color = currentColor;
                    comboText.text = "Energy + Energy";
                    suctionDetectionScript.obj0Sucked = true; // energy 1
                    suctionDetectionScript.obj1Sucked = true; // energy 2
                    suctionDetectionScript.obj2Sucked = false; // liquid 
                    notificationText.text = "Suck power Temporarily increased!";
                    // TODO create particle system to show change
                    StopCoroutine("ResetSuckForce");
                    StartCoroutine("ResetSuckForce");
                    break;
            }
        }

        IEnumerator ResetColor()
        {
            yield return new WaitForSeconds(SuckIncreaseTimeOnEnergyCollision);
            VaccumBodySingleton.Instance.GetComponent<SpriteRenderer>().color = Color.white;
            notificationText.text = "Color returned to normal";
        }
        
        IEnumerator ResetSuckForce()
        {
            yield return new WaitForSeconds(SuckIncreaseTimeOnEnergyCollision);
            SuckObjects.PowerUpSuckMultiplier = 1f;

            notificationText.text = "Suck power returned to normal";
        }
    }
}