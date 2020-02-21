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

        private Grabber _leftGrabber;
        private Grabber _rightGrabber;

        public float ThresholdUntilGrab;

        void Awake()
        {
            _input = new InputGrabber();
            LeftLeg.NewGrabberEvent  += (grabber) =>
            {
                _leftGrabber = grabber;
                Debug.Log("NewGrabberEvent");
            };
            RightLeg.NewGrabberEvent += (grabber) => _rightGrabber = grabber;
        }
        
        private void FixedUpdate()
        {
            LeftLeg.MoveLegs( _input.InGame.MoveLeftLeg.ReadValue<Vector2>());
            RightLeg.MoveLegs( _input.InGame.MoveRightLeg.ReadValue<Vector2>());

            HandleGrab(_leftGrabber, LeftLeg, _input.InGame.GrabLeft.ReadValue<float>());
            HandleGrab(_rightGrabber, RightLeg, _input.InGame.GrabRight.ReadValue<float>());
            
            //_input.InGame.GrabLeft.started += context => HandleGrab(context, _leftGrabber);
            //_input.InGame.GrabRight.performed += context => HandleGrab(context, _rightGrabber);

            //Debug.Log(_input.InGame.MoveRightLeg.ReadValue<Vector2>());
        }

        private void OnEnable() { _input.Enable(); }
        private void OnDisable() { _input.Disable(); }

        void HandleGrab(Grabber grabber, LegFactory leg, float value)
        {
            if (value > ThresholdUntilGrab)
            {
                grabber.AttemptToGrab();
            }
            else
            {
                grabber.CeaseToGrab();
            }
                

        }
    }
}