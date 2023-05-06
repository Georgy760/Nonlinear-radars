
using Common.Resources.NR_900EMS.Scripts;
using Common.Resources.Orion_2._4.Scripts.Display;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Common.Resources.Orion_2._4.Scripts
{
    public class Orion24 : MonoBehaviour
    {
        

        [SerializeField] private OrionMode _mode = OrionMode.MENU;
        [SerializeField] private DisplayManager _menuInterface;
        [SerializeField] private ModeChanger _modeChanger;
        [SerializeField] private FindObjectd _findObjectd;
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
            _controllerService.MoveOrion += MoveOrion;
        }

        public void Start()
        {
            _menuInterface.gameObject.SetActive(false); //TODO separate menu and display from each other
        }
        private void MoveOrion(float localRotate)
        {
            transform.localRotation = Quaternion.Euler(-localRotate, 180f, 0.0f); 
        }

        private void PowerUp()
        {
            switch (_mode)
            {
                case OrionMode.MENU:
                    _menuInterface.MoveUp();
                    break;
                case OrionMode.MAIN:
                    _findObjectd.PowerRadiusUp();
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
                case OrionMode.MAIN:
                    _findObjectd.PowerRadiusDown();
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
                case OrionMode.MAIN:
                    _findObjectd.VolumeUp();
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
                case OrionMode.MAIN:
                    _findObjectd.VolumeDown();
                    break;
            }
        }

        private void Power()
        {
            switch (_modeChanger.DisplayMode)
            {
                case OrionMode.MAIN:
                    _modeChanger.SwapModeTo(OrionMode.POWEROFF);
                    _mode = OrionMode.POWEROFF;
                    break;
                case OrionMode.POWEROFF:
                    _modeChanger.SwapModeTo(OrionMode.MAIN);
                    _mode = OrionMode.MAIN;
                    break;
            }
        }
        
        
    }
}
