using System;
using Helpers;
using Legs;
using UnityEngine;

    public class Blower : MonoBehaviour
    {
        [HideInInspector]
        public SLegSettings Settings;

        private Rigidbody2D _rigidbody2D;
        private InputGrabber _inputGrabber;
        [HideInInspector]
        public Vector3 LegTipDir; //normalized vec, set every frame by leg factory

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _inputGrabber = new InputGrabber();
            //do visual stuff sprite changes
            //sprite = settings.sprite
        }

        private void OnEnable() => _inputGrabber.Enable();
        private void OnDisable() => _inputGrabber.Disable();

        void Update()
        {

            float blowPressure = _inputGrabber.InGame.Blow.ReadValue<float>() * Settings.BlowBase * Settings.BlowMult;
            Vector2 blowForce = LegTipDir * blowPressure;
            
            float suckPressure = -1 * (_inputGrabber.InGame.Suck.ReadValue<float>() * Settings.SuckBase * Settings.SuckMult);
            Vector2 suckForce = LegTipDir * suckPressure;
            
            Debug.DrawLine(transform.position,transform.position + suckForce.ToVector3XY(),Color.blue);
            Debug.DrawLine(transform.position,transform.position + blowForce.ToVector3XY(),Color.red);
            _rigidbody2D.AddForce(blowForce + suckForce);
            
        }
    }
