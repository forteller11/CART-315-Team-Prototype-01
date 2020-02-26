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
                    ""name"": ""MoveTube"",
                    ""type"": ""Value"",
                    ""id"": ""a2e43d65-5d2c-453a-9aee-b14b082aa011"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateHead"",
                    ""type"": ""Value"",
                    ""id"": ""7680a0be-5f16-404b-98f7-0c6763674d80"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""NormalizeVector2,StickDeadzone"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Suck"",
                    ""type"": ""Button"",
                    ""id"": ""a27d1667-8da4-4328-97b7-84e629d20ab8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Blow"",
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
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone,NormalizeVector2"",
                    ""groups"": """",
                    ""action"": ""MoveTube"",
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
                    ""action"": ""MoveTube"",
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
                    ""action"": ""MoveTube"",
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
                    ""action"": ""MoveTube"",
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
                    ""action"": ""MoveTube"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""95bdde8a-b290-43d8-93b8-cec6abe779c9"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveTube"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""965493cf-ff64-4bc1-830f-6d260342492d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveTube"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d6675904-7c95-4488-b82c-b8e233db0239"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveTube"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""45007d48-4840-4168-b9ab-a96e5bbd083e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveTube"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f333e914-84cd-4255-86b7-4e9326ab25fe"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveTube"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""0720ad56-e90a-469a-bb31-871ff84edc71"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Suck"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0e8d5d9b-46b7-4453-854e-f436c2fffc9c"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Suck"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""77e19c0b-0d89-4eda-a9e1-4b228f7951b7"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": """",
                    ""action"": ""RotateHead"",
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
                    ""action"": ""RotateHead"",
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
                    ""action"": ""RotateHead"",
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
                    ""action"": ""RotateHead"",
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
                    ""action"": ""RotateHead"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""12f9d9d2-8ca2-46c3-af33-77b3161dac79"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateHead"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""0349b4ec-9d9e-4b55-8754-89eb680c41bd"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateHead"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""44af59c6-8699-42ac-8315-851daf5afb68"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateHead"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7e971abe-a284-4119-98c5-fc4f9a3d4372"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateHead"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""569fc00c-c26c-474e-818a-57bb42e17448"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateHead"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""138bb1a8-665d-4a16-b3ed-e204154e9d08"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Blow"",
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
        m_InGame_MoveTube = m_InGame.FindAction("MoveTube", throwIfNotFound: true);
        m_InGame_RotateHead = m_InGame.FindAction("RotateHead", throwIfNotFound: true);
        m_InGame_Suck = m_InGame.FindAction("Suck", throwIfNotFound: true);
        m_InGame_Blow = m_InGame.FindAction("Blow", throwIfNotFound: true);
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
    private readonly InputAction m_InGame_MoveTube;
    private readonly InputAction m_InGame_RotateHead;
    private readonly InputAction m_InGame_Suck;
    private readonly InputAction m_InGame_Blow;
    public struct InGameActions
    {
        private @InputGrabber m_Wrapper;
        public InGameActions(@InputGrabber wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveTube => m_Wrapper.m_InGame_MoveTube;
        public InputAction @RotateHead => m_Wrapper.m_InGame_RotateHead;
        public InputAction @Suck => m_Wrapper.m_InGame_Suck;
        public InputAction @Blow => m_Wrapper.m_InGame_Blow;
        public InputActionMap Get() { return m_Wrapper.m_InGame; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InGameActions set) { return set.Get(); }
        public void SetCallbacks(IInGameActions instance)
        {
            if (m_Wrapper.m_InGameActionsCallbackInterface != null)
            {
                @MoveTube.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnMoveTube;
                @MoveTube.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnMoveTube;
                @MoveTube.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnMoveTube;
                @RotateHead.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnRotateHead;
                @RotateHead.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnRotateHead;
                @RotateHead.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnRotateHead;
                @Suck.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnSuck;
                @Suck.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnSuck;
                @Suck.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnSuck;
                @Blow.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnBlow;
                @Blow.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnBlow;
                @Blow.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnBlow;
            }
            m_Wrapper.m_InGameActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveTube.started += instance.OnMoveTube;
                @MoveTube.performed += instance.OnMoveTube;
                @MoveTube.canceled += instance.OnMoveTube;
                @RotateHead.started += instance.OnRotateHead;
                @RotateHead.performed += instance.OnRotateHead;
                @RotateHead.canceled += instance.OnRotateHead;
                @Suck.started += instance.OnSuck;
                @Suck.performed += instance.OnSuck;
                @Suck.canceled += instance.OnSuck;
                @Blow.started += instance.OnBlow;
                @Blow.performed += instance.OnBlow;
                @Blow.canceled += instance.OnBlow;
            }
        }
    }
    public InGameActions @InGame => new InGameActions(this);
    public interface IInGameActions
    {
        void OnMoveTube(InputAction.CallbackContext context);
        void OnRotateHead(InputAction.CallbackContext context);
        void OnSuck(InputAction.CallbackContext context);
        void OnBlow(InputAction.CallbackContext context);
    }
}
