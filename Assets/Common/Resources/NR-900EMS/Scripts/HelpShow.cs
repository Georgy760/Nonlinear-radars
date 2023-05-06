using Common.Resources.NR_900EMS.Scripts.OutPutPower;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace Common.Resources.NR_900EMS.Scripts
{
    public class HelpShow : MonoBehaviour
    {

        private IControllerService _controllerService;
        [Inject]
        private void Construct(IControllerService controllerService)
        {
            _controllerService = controllerService;
            _controllerService.OnHelp += OnHelpView;
        }
        private void OnHelpView()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }

}