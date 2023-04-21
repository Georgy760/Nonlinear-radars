using System;
using Common.Resources.NR_900EMS.Scripts.Buttons;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Common.Resources.NR_900EMS.Scripts
{
    public class ControllerManager : MonoBehaviour, IControllerService
    {
        public LayerMask clickableLayer;
        
        public event Action OnMinusTap;
        public event Action OnPlusTap;
        public event Action OnLeftArrowTap;
        public event Action OnRightArrowTap;
        public event Action OnAsteriskTap;
        public event Action OnPwrTap;
        public event Action OnPowerTap;
        public event Action OnTwentyKTap;
        public event Action OnThreeDivTwoTap;
        
        public event Action OnMouseClick;
        public event Action<Vector2> MousePos;
        
        private InputActions _inputActions;
        
        private void Awake()
        {
            _inputActions = new InputActions();
            _inputActions.KeyboardEMS.Enable();
            
            _inputActions.KeyboardEMS.Minus.performed += MinusPreformed;
            _inputActions.KeyboardEMS.Plus.performed += PlusPreformed;
            _inputActions.KeyboardEMS.LeftArrow.performed += LeftArrowPreformed;
            _inputActions.KeyboardEMS.RightArrow.performed += RightArrowPreformed;
            _inputActions.KeyboardEMS.Asterisk.performed += AsteriskPreformed;
            _inputActions.KeyboardEMS.PWR.performed += PwrPreformed;
            _inputActions.KeyboardEMS.Power.performed += PowerPreformed;
            _inputActions.KeyboardEMS.twentyK.performed += TwentyK_Preformed;
            _inputActions.KeyboardEMS.ThreeDivTwo.performed += ThreeDivTwoPreformed;

            _inputActions.KeyboardEMS.MouseClick.performed += MouseClick;

        }

        private void MouseClick(InputAction.CallbackContext obj)
        {
            RaycastHit hit;
            Debug.Log($"MousePos: {_inputActions.KeyboardEMS.MousePose.ReadValue<Vector2>()}");
            if (Physics.Raycast(Camera.main.ScreenPointToRay(
                        _inputActions.KeyboardEMS.MousePose.ReadValue<Vector2>()),
                    out hit, 50, clickableLayer.value))
            {
                if (hit.collider.gameObject.GetComponent(typeof(Button)))
                {
                    hit.collider.gameObject.GetComponent<Button>().Click();
                }
            }
        }

        public void ThreeDivTwoPreformed(InputAction.CallbackContext obj)
        {
            OnThreeDivTwoTap?.Invoke();
        }

        public void TwentyK_Preformed(InputAction.CallbackContext obj)
        {
            OnTwentyKTap?.Invoke();
        }

        public void PowerPreformed(InputAction.CallbackContext obj)
        {
            OnPowerTap?.Invoke();
        }

        public void PwrPreformed(InputAction.CallbackContext obj)
        {
            OnPwrTap?.Invoke();
        }

        public void AsteriskPreformed(InputAction.CallbackContext obj)
        {
            OnAsteriskTap?.Invoke();
        }

        public void RightArrowPreformed(InputAction.CallbackContext obj)
        {
            OnRightArrowTap?.Invoke();
        }

        public void LeftArrowPreformed(InputAction.CallbackContext obj)
        {
            OnLeftArrowTap?.Invoke();
        }

        public void PlusPreformed(InputAction.CallbackContext obj)
        {
            OnPlusTap?.Invoke();
        }

        public void MinusPreformed(InputAction.CallbackContext obj)
        {
            OnMinusTap?.Invoke();
        }
    }
}
