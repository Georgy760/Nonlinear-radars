using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Common.Resources.NR_900EMS.Scripts
{
    public class FillProgressBar : MonoBehaviour
    {
        float MaxValue = 40f;
        float stepSize = 2f; //progress is done by this value
        [SerializeField] TMPro.TextMeshProUGUI FillBar;
        [SerializeField] TypeSignal _type;
        [SerializeField] TMPro.TextMeshProUGUI DisplayValue;
        private void Awake()
        {
            FindObjectd.OnNearObject += FindObject_OnNearObject;
        }
        private void FindObject_OnNearObject(float obj, TypeSignal type)
        {
            if (type != _type) return;
            float curret = (1f - obj) * MaxValue;
            int countFill = Mathf.FloorToInt(curret / stepSize);
            FillBar.text = "";
            DisplayValue.text = Convert.ToInt32(curret).ToString();
            for (int i = 0; i < countFill; i++)
            {
                FillBar.text += "=";
            }
            if (obj == 0)
            {
                FillBar.text = "";
                DisplayValue.text = "0";
            }
            
        }
    }
}