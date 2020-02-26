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
        
        
        [Header("Joint Distance")] [Range(0, 5f)]
        public float JointDistance = 0.08f;

        [Header("Force To Apply On Body")]
        public float ForceOnBody = 1;

        public float WidthMultiplier = 1f;

        [Header("Blow forces")]
        public float BlowBase = 1f;
        public float BlowMult = 2f;
        
        [Header("Suck forces")]
        public float SuckBase = 1f;
        public float SuckMult = 2f;

        [Header("Vaccum Prefabs")]
        public GameObject VaccumTipPrefab;

        [Header("Vaccum Tube Aesthetics")]
        [FormerlySerializedAs("VaccumTipColor")]
        public Color VaccumTipTint;
        [FormerlySerializedAs("VaccumSegmentColor")] public Color VaccumSegmentTint;
        public Color VaccumSegmentLineColor;

        [Header("suck")] public float SuckRadius = 5f;
        public float MaxSuck = 1f;
        public float MinSuck = 0f;
        public float SuckPosIncrease = 1f;
        [Range(0, 1)] public float VaccumeHeadRotatePercent = 0.2f;

        public int SuctionCollisionPoints = 1;
        public float MaxSuctionForce = 10f;
    }
}