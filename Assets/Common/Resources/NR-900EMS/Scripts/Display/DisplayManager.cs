using Common.Resources.NR_900EMS.Scripts.OutPutPower;
using Unity.VisualScripting;
using UnityEngine;
using System;
namespace Common.Resources.NR_900EMS.Scripts.Display
{
    public class DisplayManager : MonoBehaviour
    {
    
    
        [Header("Mode")]
        public EnumMode DisplayMode;

        private EnumMode LastDisplayMode;
    
        [SerializeField] private GameObject[] _headphones;
        public Attenuator.Attenuator Attenuator;
        [SerializeField] private OutputPower _power;
    
        [Header("Modes")] 
        [SerializeField] private GameObject Main;
        [SerializeField] private GameObject Listening;
        [SerializeField] private GameObject Mode_20K;
        [SerializeField] private GameObject Sleep;

        [SerializeField] private FindObjectd find;
        [SerializeField] private int _garmonic = 0;
        [SerializeField] private bool _headphonesStatus = false;
        [SerializeField] private bool _attenuatorStatus = false;

        public static event Action<bool> OnListen;
        private void Start()
        {
            SwapModeTo(EnumMode.POWEROFF);
            EnableHeadPhones(false);
            EnableAttenuator(false);
        }

        public void SwapModeTo(EnumMode mode)
        {
            switch (mode)
            {
                case EnumMode.MAIN:
                    Debug.Log("MAIN");
                    DisplayMode = EnumMode.MAIN;
                    Main.SetActive(true);
                    Listening.SetActive(false);
                    Mode_20K.SetActive(false);
                    Sleep.SetActive(false);
                    _power.GameObject().SetActive(true);
                    OnListen.Invoke(true);
                    _power.SetPower(Power.PMIN);
                    break;
                case EnumMode.LISTENING:
                    Debug.Log("LISTENING");
                    DisplayMode = EnumMode.LISTENING;
                    Main.SetActive(false);
                    Listening.SetActive(true);
                    Mode_20K.SetActive(false);
                    Sleep.SetActive(false);
                    _power.GameObject().SetActive(true);
                    _power.SetPower(Power.POOF);
                    break;
                case EnumMode.MODE_20K:
                    Debug.Log("MODE_20K");
                    DisplayMode = EnumMode.MODE_20K;
                    Main.SetActive(false);
                    Listening.SetActive(false);
                    Mode_20K.SetActive(true);
                    Sleep.SetActive(false);
                    _power.GameObject().SetActive(false);
                    break;
                case EnumMode.SLEEP:
                    Debug.Log("SLEEP");
                    LastDisplayMode = DisplayMode;
                    DisplayMode = EnumMode.SLEEP;
                    Main.SetActive(false);
                    Listening.SetActive(false);
                    Mode_20K.SetActive(false);
                    Sleep.SetActive(true);
                    OnListen.Invoke(false);
                    _power.GameObject().SetActive(false);
                    break;
                case EnumMode.POWEROFF:
                    Debug.Log("POWEROFF");
                    DisplayMode = EnumMode.POWEROFF;
                    Main.SetActive(false);
                    Listening.SetActive(false);
                    Mode_20K.SetActive(false);
                    Sleep.SetActive(false);
                    _power.GameObject().SetActive(false);
                    OnListen.Invoke(false);
                    _garmonic = 0;
                    break;
            }
        }

        public void ChangePower()
        {
            switch (_power.Power)
            {
                case Power.PMIN:
                    _power.SetPower(Power.PMAX);
                    find.PowerRadiusUpDown(Power.PMAX);
                    break;
                case Power.PMAX:
                    _power.SetPower(Power.PMIN);
                    find.PowerRadiusUpDown(Power.PMIN);
                    break;
            }
        }
        public void changeGarmonic()
        {
            if (_headphonesStatus)
            {
                if (_garmonic == 0)
                {
                    _headphones[0].SetActive(false);
                    _headphones[1].SetActive(true);
                    _garmonic = 1;
                }
                else
                {
                    _headphones[1].SetActive(false);
                    _headphones[0].SetActive(true);
                    _garmonic = 0;
                }
            }
        }
        public void EnableAttenuator(bool flag)
        {
            if (flag)
            {
                _attenuatorStatus = true;
                Attenuator.GameObject().SetActive(true);
            }
            else
            {
                _attenuatorStatus = false;
                Attenuator.GameObject().SetActive(false);
            }
        }
        public void EnableHeadPhones(bool flag)
        {
            if (flag)
            {
                _headphonesStatus = true;
                _headphones[_garmonic].SetActive(true);
            }
            else
            {
                _headphonesStatus = false;
                foreach (var var in _headphones)
                {
                    var.SetActive(false);
                }
            }
        }

        public void RestoreLastSession()
        {
            SwapModeTo(LastDisplayMode);
        }
    }
}
