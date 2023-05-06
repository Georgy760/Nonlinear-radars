using System;
using System.Collections;
using Common.Resources.NR_900EMS.Scripts.Buttons;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
        public event Action OnEsc;

        public event Action<float> MoveEms;
        public event Action OnHelp;

        private InputActions _inputActions;


        private bool isView = false;
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
            
            _inputActions.KeyboardEMS.ESC.performed += context => SceneManager.LoadScene("Preview");
            _inputActions.KeyboardEMS.MouseClick.performed += MouseClick;

            _inputActions.KeyboardEMS.H.performed += OnHelpView;
            _inputActions.KeyboardEMS.V.performed += OnCameraMove;

        }

        private void OnDestroy()
        {
            _inputActions.KeyboardEMS.Minus.performed -= MinusPreformed;
            _inputActions.KeyboardEMS.Plus.performed -= PlusPreformed;
            _inputActions.KeyboardEMS.LeftArrow.performed -= LeftArrowPreformed;
            _inputActions.KeyboardEMS.RightArrow.performed -= RightArrowPreformed;
            _inputActions.KeyboardEMS.Asterisk.performed -= AsteriskPreformed;
            _inputActions.KeyboardEMS.PWR.performed -= PwrPreformed;
            _inputActions.KeyboardEMS.Power.performed -= PowerPreformed;
            _inputActions.KeyboardEMS.twentyK.performed -= TwentyK_Preformed;
            _inputActions.KeyboardEMS.ThreeDivTwo.performed -= ThreeDivTwoPreformed;

            _inputActions.KeyboardEMS.ESC.performed -= context => SceneManager.LoadScene("Preview");
            _inputActions.KeyboardEMS.MouseClick.performed -= MouseClick;

            _inputActions.KeyboardEMS.H.performed -= OnHelpView;
            _inputActions.KeyboardEMS.V.performed -= OnCameraMove;
            _inputActions.KeyboardEMS.Disable();
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
            Vector3 Newpos = Camera.main.transform.localPosition - new Vector3(0, -2, 4) * koef;
            Camera.main.transform.localPosition = Newpos;
        }
        public float mouseSensitivity = 1f;
        private IEnumerator RotateCamera()
        {
            Cursor.lockState = CursorLockMode.Locked;
            while (true)
            {
                var deltaMouse = _inputActions.KeyboardEMS.DeltaMouse.ReadValue<Vector2>();
                Vector2 deltaRotation = deltaMouse * mouseSensitivity * Time.deltaTime;
                deltaRotation.y *= -1f;
                float pitchAngle = Camera.main.transform.localEulerAngles.x;
                // turns 270 deg into -90, etc
                if (pitchAngle > 180)
                    pitchAngle -= 360;
                pitchAngle = Mathf.Clamp(pitchAngle + deltaRotation.y, -90.0f, 90.0f);
                Camera.main.transform.localRotation = Quaternion.Euler(pitchAngle, 0.0f, 0.0f);
                Camera.main.transform.parent.Rotate(Vector3.up * deltaRotation.x);
                MoveEms.Invoke(pitchAngle);
                yield return new WaitForEndOfFrame();  // ∆дем следующего кадра
                if (!isView)
                {
                    Cursor.lockState = CursorLockMode.Confined;
                    break;
                }
            }
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
