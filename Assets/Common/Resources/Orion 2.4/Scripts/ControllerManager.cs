using System;
using Common.Resources.Orion_2._4.Scripts.Buttons;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Common.Resources.Orion_2._4.Scripts
{
    public class ControllerManager : MonoBehaviour, IControllerService
    {
        
        public LayerMask clickableLayer;
        
        public event Action OnPowerTap;
        public event Action OnVolumeTap;
        public event Action OnPowerUp;
        public event Action OnPowerDown;
        public event Action OnVolumeDown;
        public event Action OnVolumeUp;
        public event Action OnInput;
        public event Action OnMenu;

        public event Action OnMouseClick;
        public event Action<Vector2> MousePos;
        
        private InputActions _inputActions;

        private void Awake()
        {
            _inputActions = new InputActions();
            _inputActions.KeyboardOrion.Enable();

            _inputActions.KeyboardOrion.Power.performed += PowerTap;
            _inputActions.KeyboardOrion.Volume.performed += VolumeTap;
            _inputActions.KeyboardOrion.PowerUp.performed += PowerUpTap;
            _inputActions.KeyboardOrion.PowerDown.performed += PowerDownTap;
            _inputActions.KeyboardOrion.VolumeDown.performed += VolumeDownTap;
            _inputActions.KeyboardOrion.VolumeUp.performed += VolumeUpTap;
            _inputActions.KeyboardOrion.Input.performed += InputTap;
            _inputActions.KeyboardOrion.Menu.performed += MenuTap;

            _inputActions.KeyboardOrion.MouseClick.performed += MouseClick;
        }
        
        public void PowerTap(InputAction.CallbackContext callbackContext)
        {
            Debug.Log("PowerTap");
            OnPowerTap?.Invoke();
        }
        
        public void VolumeTap(InputAction.CallbackContext callbackContext)
        {
            Debug.Log("VolumeTap");
            OnVolumeTap?.Invoke();
        }

        
        public void PowerUpTap(InputAction.CallbackContext callbackContext)
        {
            Debug.Log("PowerUp");
            OnPowerUp?.Invoke();
        }

        
        public void PowerDownTap(InputAction.CallbackContext callbackContext)
        {
            Debug.Log("PowerDown");
            OnPowerDown?.Invoke();
        }

        
        public void VolumeDownTap(InputAction.CallbackContext callbackContext)
        {
            Debug.Log("VolumeDown");
            OnVolumeDown?.Invoke();
        }

        
        public void VolumeUpTap(InputAction.CallbackContext callbackContext)
        {
            Debug.Log("VolumeUp");
            OnVolumeUp?.Invoke();
        }

        
        public void InputTap(InputAction.CallbackContext callbackContext)
        {
            Debug.Log("Input");
            OnInput?.Invoke();
        }

        
        public void MenuTap(InputAction.CallbackContext callbackContext)
        {
            Debug.Log("Menu");
            OnMenu?.Invoke();
        }

        private void MouseClick(InputAction.CallbackContext context)
        {
            RaycastHit hit;
            Debug.Log($"MousePos: {_inputActions.KeyboardOrion.MousePose.ReadValue<Vector2>()}");
            if (Physics.Raycast(Camera.main.ScreenPointToRay(
                        _inputActions.KeyboardOrion.MousePose.ReadValue<Vector2>()),
                    out hit, 50, clickableLayer.value))
            {
                if (hit.collider.gameObject.GetComponent(typeof(Button)))
                {
                    hit.collider.gameObject.GetComponent<Button>().Click();
                }
            }
        }
    }
}