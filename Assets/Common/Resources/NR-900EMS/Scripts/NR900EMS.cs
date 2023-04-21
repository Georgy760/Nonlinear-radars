using Common.Resources.NR_900EMS.Scripts.Display;
using UnityEngine;
using Zenject;

namespace Common.Resources.NR_900EMS.Scripts
{
    public class NR900EMS : MonoBehaviour
    {
        private IControllerService _controllerService;
    

        [SerializeField] private DisplayManager _displayManager;
    
        [Inject]
        private void Construct(IControllerService controllerService)
        {
            _controllerService = controllerService;

            _controllerService.OnMinusTap += Minus;
            _controllerService.OnPlusTap += Plus;
            _controllerService.OnLeftArrowTap += LeftArrow;
            _controllerService.OnRightArrowTap += RightArrow;
            _controllerService.OnAsteriskTap += Asterisk;
            _controllerService.OnPwrTap += PWR;
            _controllerService.OnPowerTap += Power;
            _controllerService.OnTwentyKTap += TwentyK;
            _controllerService.OnThreeDivTwoTap += ThreeDivTwo;
        }

        private void OnDestroy()
        {
            _controllerService.OnMinusTap -= Minus;
            _controllerService.OnPlusTap -= Plus;
            _controllerService.OnLeftArrowTap -= LeftArrow;
            _controllerService.OnRightArrowTap -= RightArrow;
            _controllerService.OnAsteriskTap -= Asterisk;
            _controllerService.OnPwrTap -= PWR;
            _controllerService.OnPowerTap -= Power;
            _controllerService.OnTwentyKTap -= TwentyK;
            _controllerService.OnThreeDivTwoTap -= ThreeDivTwo;
        }
    
        private void Plus()
        {
            _displayManager.Attenuator.ChangeValueToNext();
        }

        private void Minus()
        {
            _displayManager.Attenuator.ChangeValueToPrev();
        }

        private void ThreeDivTwo()
        {
            _displayManager.changeGarmonic();
        }

        private void TwentyK()
        {
            switch (_displayManager.DisplayMode)
            {
                case EnumMode.MODE_20K:
                    _displayManager.SwapModeTo(EnumMode.MAIN);
                    break;
                case EnumMode.MAIN:
                    _displayManager.SwapModeTo(EnumMode.MODE_20K);
                    break;
            }
        }

        private void Power()
        {
            switch (_displayManager.DisplayMode)
            {
                case EnumMode.MAIN:
                    _displayManager.SwapModeTo(EnumMode.POWEROFF);
                    _displayManager.EnableHeadPhones(false);
                    _displayManager.EnableAttenuator(false);
                    break;
                case EnumMode.LISTENING:
                    _displayManager.SwapModeTo(EnumMode.MAIN);
                    break;
                case EnumMode.MODE_20K:
                    _displayManager.SwapModeTo(EnumMode.POWEROFF);
                    _displayManager.EnableHeadPhones(true);
                    _displayManager.EnableAttenuator(true);
                    break;
                case EnumMode.SLEEP:
                    _displayManager.RestoreLastSession();
                    _displayManager.EnableHeadPhones(true);
                    _displayManager.EnableAttenuator(true);
                    break;
                case EnumMode.POWEROFF:
                    _displayManager.SwapModeTo(EnumMode.LISTENING);
                    _displayManager.EnableHeadPhones(true);
                    _displayManager.EnableAttenuator(true);
                    break;
            }
            /*
        if (_displayManager.DisplayMode != EnumMode.SLEEP)
        {
            _displayManager.SwapModeTo(EnumMode.SLEEP);
            _displayManager.EnableHeadPhones(false);
            _displayManager.EnableAttenuator(false);
        }
        else
        {
            _displayManager.SwapModeTo(EnumMode.LISTENING);
            _displayManager.EnableHeadPhones(true);
            _displayManager.EnableAttenuator(true);
        }*/
        }

        private void PWR()
        {
            _displayManager.ChangePower();
        }

        private void Asterisk()
        {
            if (_displayManager.DisplayMode != EnumMode.POWEROFF &&
                _displayManager.DisplayMode != EnumMode.SLEEP &&
                _displayManager.DisplayMode != EnumMode.LISTENING)
            {
                _displayManager.SwapModeTo(EnumMode.SLEEP);
                _displayManager.EnableHeadPhones(false);
                _displayManager.EnableAttenuator(false);
            } 
            
        }

        private void RightArrow()
        {
            Debug.Log("RightArrow");
        }

        private void LeftArrow()
        {
            Debug.Log("LeftArrow");
        }
    }
}
