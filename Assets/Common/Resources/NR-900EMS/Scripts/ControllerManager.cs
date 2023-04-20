using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Common.Resources.NR_900EMS.Scripts
{
    public class ControllerManager : MonoBehaviour, IControllerService
    {
        public event Action OnMinusTap;
        public event Action OnPlusTap;
        public event Action OnLeftArrowTap;
        public event Action OnRightArrowTap;
        public event Action OnAsteriskTap;
        public event Action OnPwrTap;
        public event Action OnPowerTap;
        public event Action OnTwentyKTap;
        public event Action OnThreeDivTwoTap;
        
        private InputActions _InputActions;
        
        private void Awake()
        {
            _InputActions = new InputActions();
            _InputActions.KeyboardEMS.Enable();
            
            _InputActions.KeyboardEMS.Minus.performed += MinusPreformed;
            _InputActions.KeyboardEMS.Plus.performed += PlusPreformed;
            _InputActions.KeyboardEMS.LeftArrow.performed += LeftArrowPreformed;
            _InputActions.KeyboardEMS.RightArrow.performed += RightArrowPreformed;
            _InputActions.KeyboardEMS.Asterisk.performed += AsteriskPreformed;
            _InputActions.KeyboardEMS.PWR.performed += PwrPreformed;
            _InputActions.KeyboardEMS.Power.performed += PowerPreformed;
            _InputActions.KeyboardEMS.twentyK.performed += twentyK_Preformed;
            _InputActions.KeyboardEMS.ThreeDivTwo.performed += ThreeDivTwoPreformed;
            
        }

        private void ThreeDivTwoPreformed(InputAction.CallbackContext obj)
        {
            OnThreeDivTwoTap?.Invoke();
        }

        private void twentyK_Preformed(InputAction.CallbackContext obj)
        {
            OnTwentyKTap?.Invoke();
        }

        private void PowerPreformed(InputAction.CallbackContext obj)
        {
            OnPowerTap?.Invoke();
        }

        private void PwrPreformed(InputAction.CallbackContext obj)
        {
            OnPwrTap?.Invoke();
        }

        private void AsteriskPreformed(InputAction.CallbackContext obj)
        {
            OnAsteriskTap?.Invoke();
        }

        private void RightArrowPreformed(InputAction.CallbackContext obj)
        {
            OnRightArrowTap?.Invoke();
        }

        private void LeftArrowPreformed(InputAction.CallbackContext obj)
        {
            OnLeftArrowTap?.Invoke();
        }

        private void PlusPreformed(InputAction.CallbackContext obj)
        {
            OnPlusTap?.Invoke();
        }

        private void MinusPreformed(InputAction.CallbackContext obj)
        {
            OnMinusTap?.Invoke();
        }
    }
}
