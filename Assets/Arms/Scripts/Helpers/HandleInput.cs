using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Helpers
{
    public class HandleInput : MonoBehaviour
    {
        private InputGrabber _input;

        public LegFactory LeftLeg;
        public LegFactory RightLeg;

        private Blower _leftBlower;
        private Blower _rightBlower;

        public float ThresholdUntilGrab;

        void Awake()
        {
            _input = new InputGrabber();
        }
        
        private void Update()
        {
            LeftLeg.MoveLegs( _input.InGame.MoveLeftLeg.ReadValue<Vector2>());

            //_input.InGame.GrabLeft.started += context => HandleGrab(context, _leftGrabber);
            //_input.InGame.GrabRight.performed += context => HandleGrab(context, _rightGrabber);

            //Debug.Log(_input.InGame.MoveRightLeg.ReadValue<Vector2>());
        }

        private void OnEnable() { _input.Enable(); }
        private void OnDisable() { _input.Disable(); }
        
    }
}