using System;
using System.Collections;
using System.Threading;
using Common.Resources.Orion_2._4.Scripts.Buttons;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
        public event Action OnHelp;
        public event Action OnMouseClick;
        public event Action<Vector2> MousePos;
        public event Action OnEsc;
        public event Action<float> MoveOrion;
        private InputActions _inputActions;
        

        private bool isView = false;
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
            _inputActions.KeyboardOrion.V.performed += OnCameraMove;
            _inputActions.KeyboardOrion.ESC.performed += context => SceneManager.LoadScene("Preview");
            _inputActions.KeyboardOrion.MouseClick.performed += MouseClick;
            _inputActions.KeyboardOrion.H.performed += OnHelpView;
        }
        private void OnDestroy()
        {
            _inputActions.KeyboardOrion.Power.performed -= PowerTap;
            _inputActions.KeyboardOrion.Volume.performed -= VolumeTap;
            _inputActions.KeyboardOrion.PowerUp.performed -= PowerUpTap;
            _inputActions.KeyboardOrion.PowerDown.performed -= PowerDownTap;
            _inputActions.KeyboardOrion.VolumeDown.performed -= VolumeDownTap;
            _inputActions.KeyboardOrion.VolumeUp.performed -= VolumeUpTap;
            _inputActions.KeyboardOrion.Input.performed -= InputTap;
            _inputActions.KeyboardOrion.Menu.performed -= MenuTap;
            _inputActions.KeyboardOrion.V.performed -= OnCameraMove;
            _inputActions.KeyboardOrion.ESC.performed -= context => SceneManager.LoadScene("Preview");
            _inputActions.KeyboardOrion.MouseClick.performed -= MouseClick;
            _inputActions.KeyboardOrion.H.performed -= OnHelpView;
            _inputActions.KeyboardOrion.Disable();
        }
        public void OnHelpView(InputAction.CallbackContext callbackContext)
        {

            Debug.Log("HelpTap");
            OnHelp?.Invoke();
        }

        public void OnCameraMove(InputAction.CallbackContext callbackContext)
        {
            float koef;
            if (!isView)
            {
                isView = true;
                koef = 1f;
                StartCoroutine(RotateCamera());
            }
            else
            {
                isView = false;
                koef = -1f;
            }
            Vector3 Newpos = Camera.main.transform.localPosition - new Vector3(0, -1, 3) * koef;
            Camera.main.transform.localPosition = Newpos;
        }
        public void OnEscTap(InputAction.CallbackContext callbackContext)
        {
            Debug.Log("EscTap");
            OnEsc?.Invoke();
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
        public float mouseSensitivity = 1f;


        private IEnumerator RotateCamera()
        {
            Cursor.lockState = CursorLockMode.Locked;
            while (true)
            {
                var deltaMouse = _inputActions.KeyboardOrion.DeltaMouse.ReadValue<Vector2>();
                Vector2 deltaRotation = deltaMouse * mouseSensitivity * Time.deltaTime;
                deltaRotation.y *= -1f;
                float pitchAngle = Camera.main.transform.localEulerAngles.x;
                // turns 270 deg into -90, etc
                if (pitchAngle > 180)
                    pitchAngle -= 360;
                pitchAngle = Mathf.Clamp(pitchAngle + deltaRotation.y, -90.0f, 90.0f);
                Camera.main.transform.localRotation = Quaternion.Euler(pitchAngle, 0.0f, 0.0f);
                Camera.main.transform.parent.Rotate(Vector3.up * deltaRotation.x);
                MoveOrion.Invoke(pitchAngle);
                yield return new WaitForEndOfFrame();  // ∆дем следующего кадра
                if (!isView)
                {
                    Cursor.lockState = CursorLockMode.Confined;
                    break;
                }
            }
        }
    }
}