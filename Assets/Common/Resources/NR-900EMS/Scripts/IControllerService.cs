using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Common.Resources.NR_900EMS.Scripts
{
    public interface IControllerService
    {
        event Action OnMinusTap;
        event Action OnPlusTap;
        event Action OnLeftArrowTap;
        event Action OnRightArrowTap;
        event Action OnAsteriskTap;
        event Action OnPwrTap;
        event Action OnPowerTap;
        event Action OnTwentyKTap;
        event Action OnThreeDivTwoTap;
        public void ThreeDivTwoPreformed(InputAction.CallbackContext callbackContext);
        public void TwentyK_Preformed(InputAction.CallbackContext obj);
        public void PowerPreformed(InputAction.CallbackContext obj);
        public void PwrPreformed(InputAction.CallbackContext obj);
        public void AsteriskPreformed(InputAction.CallbackContext obj);
        public void RightArrowPreformed(InputAction.CallbackContext obj);
        public void LeftArrowPreformed(InputAction.CallbackContext obj);
        public void PlusPreformed(InputAction.CallbackContext obj);
        public void MinusPreformed(InputAction.CallbackContext obj);
        
        public event Action OnMouseClick;
        public event Action<Vector2> MousePos;
    }
}
