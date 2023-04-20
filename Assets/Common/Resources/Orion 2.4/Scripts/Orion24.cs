using Common.Resources.Orion_2._4.Scripts.Display;
using UnityEngine;
using Zenject;

namespace Common.Resources.Orion_2._4.Scripts
{
    public class Orion24 : MonoBehaviour
    {
        

        [SerializeField] private OrionMode _mode = OrionMode.MENU;
        [SerializeField] private DisplayManager _menuInterface;
        
        private IControllerService _controllerService;
        [Inject]
        private void Construct(IControllerService controllerService)
        {
            _controllerService = controllerService;
            _controllerService.OnPowerTap += Power;
            _controllerService.OnVolumeDown += VolumeDown;
            _controllerService.OnVolumeUp += VolumeUp;
            _controllerService.OnPowerDown += PowerDown;
            _controllerService.OnPowerUp += PowerUp;
        }

        public void Start()
        {
            _menuInterface.gameObject.SetActive(false); //TODO separate menu and display from each other
        }

        private void PowerUp()
        {
            switch (_mode)
            {
                case OrionMode.MENU:
                    _menuInterface.MoveUp();
                    break;
            }
        }

        private void PowerDown()
        {
            switch (_mode)
            {
                case OrionMode.MENU:
                    _menuInterface.MoveDown();
                    break;
            }
        }

        private void VolumeUp()
        {
            switch (_mode)
            {
                case OrionMode.MENU:
                    _menuInterface.MoveRight();
                    break;
            }
        }

        private void VolumeDown()
        {
            switch (_mode)
            {
                case OrionMode.MENU:
                    _menuInterface.MoveLeft();
                    break;
            }
        }

        private void Power()
        {
            _menuInterface.gameObject.SetActive(!_menuInterface.gameObject.activeSelf);
        }
        
        
    }
}
