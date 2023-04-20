using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Zenject;

namespace Common.Resources.Orion_2._4.Scripts.Buttons
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
                case ButtonType.POWER:
                    _controllerManager.OnPowerTap += PowerAnimate;
                    break;
                case ButtonType.VOLUME:
                    _controllerManager.OnVolumeTap += VolumeAnimate;
                    break;
                case ButtonType.POWER_UP:
                    _controllerManager.OnPowerUp += PowerUpAnimate;
                    break;
                case ButtonType.POWER_DOWN:
                    _controllerManager.OnPowerDown += PowerDownAnimate;
                    break;
                case ButtonType.VOLUME_DOWN:
                    _controllerManager.OnVolumeDown += VolumeDownAnimate;
                    break;
                case ButtonType.VOLUME_UP:
                    _controllerManager.OnVolumeUp += VolumeUpAnimate;
                    break;
                case ButtonType.INPUT:
                    _controllerManager.OnInput += InputAnimate; 
                    break;
                case ButtonType.MENU:
                    _controllerManager.OnMenu += MenuAnimate;
                    break;
            }
        }

        private void OnDestroy()
        {
            _controllerManager.OnPowerTap -= PowerAnimate;
            _controllerManager.OnVolumeTap -= VolumeAnimate;
            _controllerManager.OnPowerUp -= PowerUpAnimate;
            _controllerManager.OnPowerDown -= PowerDownAnimate;
            _controllerManager.OnVolumeDown -= VolumeDownAnimate;
            _controllerManager.OnVolumeUp -= VolumeUpAnimate;
            _controllerManager.OnInput -= InputAnimate;
            _controllerManager.OnMenu -= MenuAnimate;
        }
        public void Click()
        {
            switch (_buttonType)
            {
                case ButtonType.POWER:
                    _controllerManager.PowerTap(new InputAction.CallbackContext());
                    
                    break;
                case ButtonType.VOLUME:
                    _controllerManager.VolumeTap(new InputAction.CallbackContext());
                    break;
                case ButtonType.POWER_UP:
                    _controllerManager.PowerUpTap(new InputAction.CallbackContext());
                    break;
                case ButtonType.POWER_DOWN:
                    _controllerManager.PowerDownTap(new InputAction.CallbackContext());
                    break;
                case ButtonType.VOLUME_DOWN:
                    _controllerManager.VolumeDownTap(new InputAction.CallbackContext());
                    break;
                case ButtonType.VOLUME_UP:
                    _controllerManager.VolumeUpTap(new InputAction.CallbackContext());
                    break;
                case ButtonType.INPUT:
                    _controllerManager.InputTap(new InputAction.CallbackContext());
                    break;
                case ButtonType.MENU:
                    _controllerManager.MenuTap(new InputAction.CallbackContext());
                    break;
            }
        }

        private void MenuAnimate()
        {
        
        }
        
        private void InputAnimate()
        {
        
        }
        
        private void VolumeUpAnimate()
        {
        
        }

        private void VolumeDownAnimate()
        {
        
        }

        private void PowerDownAnimate()
        {
        
        }
        
        private void PowerUpAnimate()
        {
        
        }
        
        private void VolumeAnimate()
        {
        
        }
        
        public void PowerAnimate()
        {
            
        }
    }
}