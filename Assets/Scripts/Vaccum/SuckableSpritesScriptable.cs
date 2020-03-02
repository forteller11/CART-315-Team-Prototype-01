using System.Collections.Generic;
using UnityEngine;

namespace Legs
{
    [CreateAssetMenu(fileName = "Suckable Sprites Settings", menuName = "ScriptableObjects/Suckable Sprites Settings", order = 1)]
    public class SuckableSpritesScriptable : ScriptableObject
    {
        public List<Sprite> NonCombinableSprites;
        public List<Sprite> EnergySprites;
        public List<Sprite> LiquidSprites;
        public List<Sprite> ClothesSprites;
    }
}