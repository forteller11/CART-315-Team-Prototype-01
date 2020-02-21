// GENERATED AUTOMATICALLY FROM 'Assets/Arms/Scripts/Legs/InputGrabber.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputGrabber : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputGrabber()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputGrabber"",
    ""maps"": [
        {
            ""name"": ""InGame"",
            ""id"": ""de3de1bf-7d2d-43d6-a067-8ec44de69679"",
            ""actions"": [
                {
                    ""name"": ""MoveLeftLeg"",
                    ""type"": ""Value"",
                    ""id"": ""a2e43d65-5d2c-453a-9aee-b14b082aa011"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveRightLeg"",
                    ""type"": ""Value"",
                    ""id"": ""7680a0be-5f16-404b-98f7-0c6763674d80"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GrabLeft"",
                    ""type"": ""Button"",
                    ""id"": ""a27d1667-8da4-4328-97b7-84e629d20ab8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GrabRight"",
                    ""type"": ""Button"",
                    ""id"": ""7e914bd0-11d2-4e50-9c1a-04677901c8c7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""b0f18489-8334-466a-8634-99cbdd675682"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveLeftLeg"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""94c742a2-c701-4026-a51d-9ff77c74ff7c"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveLeftLeg"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""73adebe9-b1e9-4af2-b589-57711152a6e6"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveLeftLeg"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""da30dbc0-e6d3-494b-805a-26acb9335f9d"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveLeftLeg"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""688eaef9-c3b1-4707-a37b-3511309da713"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveLeftLeg"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""0720ad56-e90a-469a-bb31-871ff84edc71"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GrabLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""77e19c0b-0d89-4eda-a9e1-4b228f7951b7"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveRightLeg"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""016ae2b6-ba57-4b29-b7bb-102a7a93fd07"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveRightLeg"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""6b998e4d-95d6-408f-b566-eafd732915ed"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveRightLeg"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7862c4e9-ce2b-4d90-a673-ac82ec543e5f"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveRightLeg"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""5c8bd64e-3a5c-4560-9b53-3c3c311dea96"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveRightLeg"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""138bb1a8-665d-4a16-b3ed-e204154e9d08"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GrabRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // InGame
        m_InGame = asset.FindActionMap("InGame", throwIfNotFound: true);
        m_InGame_MoveLeftLeg = m_InGame.FindAction("MoveLeftLeg", throwIfNotFound: true);
        m_InGame_MoveRightLeg = m_InGame.FindAction("MoveRightLeg", throwIfNotFound: true);
        m_InGame_GrabLeft = m_InGame.FindAction("GrabLeft", throwIfNotFound: true);
        m_InGame_GrabRight = m_InGame.FindAction("GrabRight", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // InGame
    private readonly InputActionMap m_InGame;
    private IInGameActions m_InGameActionsCallbackInterface;
    private readonly InputAction m_InGame_MoveLeftLeg;
    private readonly InputAction m_InGame_MoveRightLeg;
    private readonly InputAction m_InGame_GrabLeft;
    private readonly InputAction m_InGame_GrabRight;
    public struct InGameActions
    {
        private @InputGrabber m_Wrapper;
        public InGameActions(@InputGrabber wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveLeftLeg => m_Wrapper.m_InGame_MoveLeftLeg;
        public InputAction @MoveRightLeg => m_Wrapper.m_InGame_MoveRightLeg;
        public InputAction @GrabLeft => m_Wrapper.m_InGame_GrabLeft;
        public InputAction @GrabRight => m_Wrapper.m_InGame_GrabRight;
        public InputActionMap Get() { return m_Wrapper.m_InGame; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InGameActions set) { return set.Get(); }
        public void SetCallbacks(IInGameActions instance)
        {
            if (m_Wrapper.m_InGameActionsCallbackInterface != null)
            {
                @MoveLeftLeg.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnMoveLeftLeg;
                @MoveLeftLeg.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnMoveLeftLeg;
                @MoveLeftLeg.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnMoveLeftLeg;
                @MoveRightLeg.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnMoveRightLeg;
                @MoveRightLeg.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnMoveRightLeg;
                @MoveRightLeg.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnMoveRightLeg;
                @GrabLeft.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnGrabLeft;
                @GrabLeft.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnGrabLeft;
                @GrabLeft.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnGrabLeft;
                @GrabRight.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnGrabRight;
                @GrabRight.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnGrabRight;
                @GrabRight.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnGrabRight;
            }
            m_Wrapper.m_InGameActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveLeftLeg.started += instance.OnMoveLeftLeg;
                @MoveLeftLeg.performed += instance.OnMoveLeftLeg;
                @MoveLeftLeg.canceled += instance.OnMoveLeftLeg;
                @MoveRightLeg.started += instance.OnMoveRightLeg;
                @MoveRightLeg.performed += instance.OnMoveRightLeg;
                @MoveRightLeg.canceled += instance.OnMoveRightLeg;
                @GrabLeft.started += instance.OnGrabLeft;
                @GrabLeft.performed += instance.OnGrabLeft;
                @GrabLeft.canceled += instance.OnGrabLeft;
                @GrabRight.started += instance.OnGrabRight;
                @GrabRight.performed += instance.OnGrabRight;
                @GrabRight.canceled += instance.OnGrabRight;
            }
        }
    }
    public InGameActions @InGame => new InGameActions(this);
    public interface IInGameActions
    {
        void OnMoveLeftLeg(InputAction.CallbackContext context);
        void OnMoveRightLeg(InputAction.CallbackContext context);
        void OnGrabLeft(InputAction.CallbackContext context);
        void OnGrabRight(InputAction.CallbackContext context);
    }
}
