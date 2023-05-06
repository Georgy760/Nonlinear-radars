using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Common.Resources.Orion_2._4.Scripts
{
    public class TypeSignalChanger : MonoBehaviour
    {
        TMPro.TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
            FindObjectd.OnChangeTypeObject += ReciveFindType;
        }

        void ReciveFindType(TypeSignal type)
        {
            switch (type)
            {
                case TypeSignal.None:
                    _text.text = "";
                    break;
                case TypeSignal.Semi:
                    _text.text = "SEMICOND";
                    break;
                case TypeSignal.Corr:
                    _text.text = "CORRISIVE";
                    break;
            }
        }


    }
}