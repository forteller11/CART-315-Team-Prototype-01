using UnityEngine;

namespace Legs
{
    public class CombineManager : MonoBehaviour
    {
        private SuckObjects SuckObjects;
        [SerializeField] private float SuckIncreaseOnEnergyCollision = 2f;

        void Start()
        {
            SuckObjects = GameObject.Find("SuckObjects").GetComponent<SuckObjects>();
            if (SuckObjects == null)
                Debug.LogError("Suck Objects not found, has the name of the prefab been changed?");
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
                    //TODO start coroutine to force multiplier in settings.... at end it changes it back
                    break;
            
                //if a clothe and energy type combine...
                case Swallowable.CombinableType.Energy | Swallowable.CombinableType.Energy:
                    s1.Pool.ReturnToPool(s1.gameObject);
                    s2.Pool.ReturnToPool(s2.gameObject);
                    SuckObjects.SuckMultiplier *= SuckIncreaseOnEnergyCollision;
                    break;
            }
        }
    }
}