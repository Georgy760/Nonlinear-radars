using Common.Resources.NR_900EMS.Scripts.Display;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Common.Resources.Orion_2._4.Scripts.Display
{
    public class ModeChanger : MonoBehaviour
    {

        [Header("Mode")]
        public OrionMode DisplayMode;

        [Header("Modes")]
        [SerializeField] private GameObject Main;
        [SerializeField] private GameObject Setting;
        [SerializeField] private GameObject Menu;

        public static Action<bool> OnListen;
        private void Awake()
        {
            SwapModeTo(OrionMode.POWEROFF);
        }
        public void SwapModeTo(OrionMode mode)
        {
            switch (mode)
            {
                case OrionMode.MAIN:
                    Debug.Log("MAIN");
                    DisplayMode = OrionMode.MAIN;
                    Main.SetActive(true);
                    Setting.SetActive(false);
                    Menu.SetActive(false);
                    OnListen?.Invoke(true);
                    break;
                case OrionMode.MENU:
                    Debug.Log("LISTENING");
                    DisplayMode = OrionMode.MENU;

                    Menu.SetActive(false);
                    Setting.SetActive(true);
                    Main.SetActive(false);
                    OnListen?.Invoke(false);
                    break;
                case OrionMode.POWEROFF:
                    Debug.Log("POWEROFF");
                    DisplayMode = OrionMode.POWEROFF;
                    Main.SetActive(false);
                    Setting.SetActive(false);
                    Menu.SetActive(false);
                    OnListen?.Invoke(false);
                    break;
            }
        }
    }

}