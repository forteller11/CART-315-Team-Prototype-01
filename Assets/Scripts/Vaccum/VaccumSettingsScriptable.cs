using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Legs
{
    [CreateAssetMenu(fileName = "Vaccum Settings", menuName = "ScriptableObjects/Vaccum Settings", order = 1)]
    public class VaccumSettingsScriptable : ScriptableObject
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
        
        [Header("Moving arms force")]
        [SerializeField]
        [FormerlySerializedAs("MaxForce")]
        public float OpenMaxForce = 1f;
        [FormerlySerializedAs("ForceCurve")] [SerializeField]
        public AnimationCurve OpenForceCurve = new AnimationCurve(DEFAULT_KEYFRAME);

        [Header("Moving Arms velocity")] public float MaxTubeVelocity = 1f;
        
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

        [Header("Force To Apply On Body On Suction")]
        public float ForceOnBody = 1;

        public float WidthMultiplier = 1f;
        
        [Header("Vaccum Prefabs")]
        public GameObject VaccumTipPrefab;

        [Header("Vaccum Tube Aesthetics")]
        [FormerlySerializedAs("VaccumTipColor")]
        public Color VaccumTipTint;
        [FormerlySerializedAs("VaccumSegmentColor")] public Color VaccumSegmentTint;
        public Color VaccumSegmentLineColor;

        [Header("Sucking/Vaccuming of object")] 
        public float SuckRadius = 5f;
        public AnimationCurve SuckCurve = new AnimationCurve(DEFAULT_KEYFRAME);
        public float MaxSuck = 1f;

        [Header("Suction to walls")] 
        [Range(0, 1)] public float VaccumeHeadRotatePercent = 0.2f;
        public float MaxSuctionForce = 10f;
        public float AngularDampeningOnSuction = 15f;
        public float AngularDampeningOnNoSuction = 10_000f;
    }
}