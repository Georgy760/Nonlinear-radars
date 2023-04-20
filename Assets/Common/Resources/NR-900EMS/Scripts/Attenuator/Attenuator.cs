using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Attenuator : MonoBehaviour
{
    [SerializeField] private string[] _attValue = {"0","10","20","30","40","50"};
    [SerializeField] private TMP_Text _textMeshPro;
    [SerializeField] private int currentVal = 0;

    private void Awake()
    {
        _textMeshPro.text = _attValue[0] + "db";
    }
    
    public void ChangeValueToNext()
    {
        if (currentVal < _attValue.Length-1)
        {
            currentVal++;
            _textMeshPro.text = _attValue[currentVal] + "db";
            Debug.Log(_attValue[currentVal] + "db");
        }
    }
    public void ChangeValueToPrev()
    {
        if (currentVal > 0)
        {
            currentVal--;
            _textMeshPro.text = _attValue[currentVal] + "db";
            Debug.Log(_attValue[currentVal] + "db");
        }
    }
}
