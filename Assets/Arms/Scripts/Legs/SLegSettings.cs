using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Legs
{
    [CreateAssetMenu(fileName = "Leg Settings", menuName = "ScriptableObjects/Leg Settings", order = 1)]
    public class SLegSettings : ScriptableObject
    {
        private static Keyframe DEFAULT_KEYFRAME = new Keyframe(0.5f, 1f);
        
        [Header("Scale")]
        [SerializeField]
        public Vector3 MaxScale = Vector3.one * 4f;
        [SerializeField]
        public AnimationCurve ScaleCurve = new AnimationCurve(DEFAULT_KEYFRAME);
        
        [Header("Mass")]
        [SerializeField]
        public float MaxMass = 1f;
        [SerializeField]
        public AnimationCurve MassCurve = new AnimationCurve(DEFAULT_KEYFRAME);
        
        [Header("Force When Not Grabbing")]
        [SerializeField]
        [FormerlySerializedAs("MaxForce")]
        public float OpenMaxForce = 1f;
        [FormerlySerializedAs("ForceCurve")] [SerializeField]
        public AnimationCurve OpenForceCurve = new AnimationCurve(DEFAULT_KEYFRAME);
        
        [Header("Force On Grab")]
        [SerializeField]
        public float ClosedMaxForce = 1f;
        public AnimationCurve ClosedForceCurve = new AnimationCurve(DEFAULT_KEYFRAME);
        
        [Header("Spring Frequency")]
        [SerializeField]
        public float MaxFrequency = 5f;
        [SerializeField]
        public AnimationCurve FrequencyCurve = new AnimationCurve(DEFAULT_KEYFRAME);
        
        [Header("Spring Dampening")]
        [SerializeField]
        public float MaxDampening = 1f;
        [SerializeField]
        public AnimationCurve DampeningCurve = new AnimationCurve(DEFAULT_KEYFRAME);
        
        [Header("Gravity Scale")]
        [SerializeField]
        public float MaxGravity = 1f;
        [SerializeField]
        public AnimationCurve GravityCurve = new AnimationCurve(DEFAULT_KEYFRAME);
        
        [Header("Joint Spacing")] [Range(0, 1)]
        public float JointSpacing = 0.2f;
        
        [Header("Joint Distance")] [Range(0, .6f)]
        public float JointDistance = 0.08f;

        [Header("Force To Apply On Body")]
        public float ForceOnBody = 1;

        public float WidthMultiplier = 1f;
        public AnimationCurve WidthCurve = new AnimationCurve(DEFAULT_KEYFRAME);
        
        [Header("Blow forces")]
        public float BlowBase = 1f;
        public float BlowMult = 2f;
        
        [Header("Suck forces")]
        public float SuckBase = 1f;
        public float SuckMult = 2f;

        [Header("Vaccum Sprites")]
        public Sprite VaccumTipSprite;
        public Sprite VaccumSegmentSprite;
    }
}