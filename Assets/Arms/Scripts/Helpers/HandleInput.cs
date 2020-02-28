﻿using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Helpers
{
    public class HandleInput : MonoBehaviour
    {
        private InputGrabber _input;

        public LegFactory Tube;


        void Awake()
        {
            _input = new InputGrabber();
        }
        
        private void Update()
        {
            float suckInput = _input.InGame.Suck.ReadValue<float>();
            
            Tube.MoveLegs( _input.InGame.MoveTube.ReadValue<Vector2>(), suckInput);
            Tube.RotateVaccumHead(_input.InGame.RotateHead.ReadValue<Vector2>());
            Tube.SuckVaccumables( suckInput);
            
            //_input.InGame.GrabLeft.started += context => HandleGrab(context, _leftGrabber);
            //_input.InGame.GrabRight.performed += context => HandleGrab(context, _rightGrabber);

            //Debug.Log(_input.InGame.MoveRightLeg.ReadValue<Vector2>());
        }

        private void OnEnable() { _input.Enable(); }
        private void OnDisable() { _input.Disable(); }
        
    }
}