using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Common.Scenes.Preview.Scripts
{
    public class ControllerManager : MonoBehaviour, IControllerService
    {
        public event Action OnPrevModel;
        public event Action OnNextModel;
        public event Action OnSelectModel;
        
        private InputActions _InputActions;
        
        private void Awake()
        {
            _InputActions = new InputActions();
            _InputActions.Preview.Enable();

            _InputActions.Preview.PrevModel.performed += PrevModel;
            _InputActions.Preview.NextModel.performed += NextModel;
            _InputActions.Preview.Select.performed += Select;

        }

        private void PrevModel(InputAction.CallbackContext obj)
        {
            OnPrevModel?.Invoke();
        }
        private void NextModel(InputAction.CallbackContext obj)
        {
            OnNextModel?.Invoke();
        }
        private void Select(InputAction.CallbackContext obj)
        {
            OnSelectModel?.Invoke();
        }
    }
}
