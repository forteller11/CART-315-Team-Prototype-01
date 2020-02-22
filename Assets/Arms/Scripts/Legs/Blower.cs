using System;
using Legs;
using UnityEngine;

    public class Blower : MonoBehaviour
    {
        [HideInInspector]
        public SLegSettings Settings;

        private Rigidbody2D _rigidbody2D;
        private InputGrabber _inputGrabber;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            //do visual stuff sprite changes
            //sprite = settings.sprite
        }

        private void OnEnable() => _inputGrabber.Enable();
        private void OnDisable() => _inputGrabber.Disable();

        void Update()
        {
            Debug.DrawLine(transform.position,transform.position+transform.forward);
            
            float zRot = transform.rotation.eulerAngles.z;
            float xx = Mathf.Cos(zRot);
            float yy = Mathf.Sin(zRot);
            
            float blowPressure = (_inputGrabber.InGame.Blow.ReadValue<float>() * Settings.BlowBase * Settings.BlowMult);
            Vector2 blowForce = new Vector2(xx,yy) * blowPressure;
            
            float suckPressure = -1 * (_inputGrabber.InGame.Suck.ReadValue<float>() * Settings.SuckBase * Settings.SuckMult);
            Vector2 suckForce = new Vector2(xx,yy) * suckPressure;
            
            _rigidbody2D.AddForce(blowForce + suckForce);
            
        }
    }
