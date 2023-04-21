using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Common.Resources.NR_900EMS.Scripts.Buttons
{
    public class Button : MonoBehaviour
    {
        [SerializeField] private ButtonType _buttonType;
        private IControllerService _controllerManager;


        [Inject]
        void Construct(IControllerService controllerService)
        {
            _controllerManager = controllerService;
            switch (_buttonType)
            {
                case ButtonType.MINUS:
                    _controllerManager.OnMinusTap += MinusAnimate;
                    break;
                case ButtonType.PLUS:
                    _controllerManager.OnPlusTap += PlusAnimate;
                    break;
                case ButtonType.LEFT_ARROW:
                    _controllerManager.OnLeftArrowTap += LeftArrowAnimate;
                    break;
                case ButtonType.RIGHT_ARROW:
                    _controllerManager.OnRightArrowTap += RightArrowAnimate;
                    break;
                case ButtonType.ASTERISK:
                    _controllerManager.OnAsteriskTap += AsteriskAnimate;
                    break;
                case ButtonType.PWR:
                    _controllerManager.OnPwrTap += PwrAnimate;
                    break;
                case ButtonType.POWER:
                    _controllerManager.OnPowerTap += PowerAnimate;
                    break;
                case ButtonType.TWENTY_K:
                    _controllerManager.OnTwentyKTap += TwentyKAnimate;
                    break;
                case ButtonType.THREE_DIV_TWO:
                    _controllerManager.OnThreeDivTwoTap += ThreeDivTwo;
                    break;
                
            }
        }

        private void OnDestroy()
        {
            _controllerManager.OnPowerTap -= PowerAnimate;
        }
        public void Click()
        {
            switch (_buttonType)
            {
                case ButtonType.MINUS:
                    _controllerManager.MinusPreformed(new InputAction.CallbackContext());
                    break;
                case ButtonType.PLUS:
                    _controllerManager.PlusPreformed(new InputAction.CallbackContext());
                    break;
                case ButtonType.LEFT_ARROW:
                    _controllerManager.LeftArrowPreformed(new InputAction.CallbackContext());
                    break;
                case ButtonType.RIGHT_ARROW:
                    _controllerManager.RightArrowPreformed(new InputAction.CallbackContext());
                    break;
                case ButtonType.ASTERISK:
                    _controllerManager.AsteriskPreformed(new InputAction.CallbackContext());
                    break;
                case ButtonType.PWR:
                    _controllerManager.PwrPreformed(new InputAction.CallbackContext());
                    break;
                case ButtonType.POWER:
                    _controllerManager.PowerPreformed(new InputAction.CallbackContext());
                    break;
                case ButtonType.TWENTY_K:
                    _controllerManager.TwentyK_Preformed(new InputAction.CallbackContext());
                    break;
                case ButtonType.THREE_DIV_TWO:
                    _controllerManager.ThreeDivTwoPreformed(new InputAction.CallbackContext());
                    break;
            }
        }

        private void MinusAnimate()
        {
        
        }
        
        private void PlusAnimate()
        {
        
        }
        
        private void LeftArrowAnimate()
        {
        
        }

        private void RightArrowAnimate()
        {
        
        }

        private void AsteriskAnimate()
        {
        
        }
        
        private void PwrAnimate()
        {
        
        }
        
        private void PowerAnimate()
        {
        
        }
        
        public void TwentyKAnimate()
        {
            
        }
        
        public void ThreeDivTwo()
        {
            
        }
        
    }
}