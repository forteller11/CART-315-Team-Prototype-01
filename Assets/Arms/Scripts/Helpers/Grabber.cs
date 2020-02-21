using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Helpers
{
    [RequireComponent(typeof(HingeJoint2D))]
    public class Grabber : MonoBehaviour
    {
        private HingeJoint2D _grabJoint;
        private bool _attemptGrab;
        public static float SCALE_MULTIPLY = 3f;
        public event Action<bool> ChangedGrabbedStateEvent;
        private SpriteRenderer _spriteRenderer;
        private static Color CLOSED_COLOR = new Color(1f, 0.95f, 0.68f);
        //private static Color OPEN_COLOR = new Color(1,1,1,1);
        public Color OpenColor;
        public Color AttemptGrabColor;

        void Awake()
        {
            _grabJoint = GetComponent<HingeJoint2D>();
            _grabJoint.enabled = false;
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }


        private void OnTriggerStay2D(Collider2D other)
        {

            if (_attemptGrab == false)
                return;
            if (other.GetComponent<Segment>() != null)
                return;

            if (other.gameObject.CompareTag("UnGrabbable"))
                return;
            
            var rbOther = other.GetComponent<Rigidbody2D>();
            if (rbOther != null)
            {
                _spriteRenderer.color = CLOSED_COLOR;
                _grabJoint.enabled = true;
                _grabJoint.connectedBody = rbOther;
                ChangedGrabbedStateEvent?.Invoke(true);
            }

        }


        public void AttemptToGrab()
        {
            _attemptGrab = true;
            if (_grabJoint.connectedBody == null)
                _spriteRenderer.color = AttemptGrabColor;
        }
        public void CeaseToGrab()
        {
            _attemptGrab = false;
            _spriteRenderer.color = OpenColor;
            if (_grabJoint.connectedBody != null)
            {
                _grabJoint.connectedBody = null;
                _grabJoint.enabled = false;
                ChangedGrabbedStateEvent?.Invoke(false);
            }
 
        }
    }
}